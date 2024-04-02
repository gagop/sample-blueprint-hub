using System.Text.RegularExpressions;
using Gakko.API.Context;
using Gakko.API.DTOs;
using Gakko.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Gakko.API.Services;

public class RecruitmentsService : IRecruitmentsService
{
    private readonly IAppointmentManagerService _appointmentManagerService;
    private readonly GakkoContext _dbContext;
    private readonly IEmailService _emailService;

    public RecruitmentsService(GakkoContext dbContext, IAppointmentManagerService appointmentManagerService,
        IEmailService emailService)
    {
        _dbContext = dbContext;
        _appointmentManagerService = appointmentManagerService;
        _emailService = emailService;
    }

    public async Task<Student> CreateRecruitment(CreateRecruitmentDto createRecruitmentDto)
    {
        if (string.IsNullOrWhiteSpace(createRecruitmentDto.Pesel) ||
            string.IsNullOrWhiteSpace(createRecruitmentDto.Passport))
            throw new ArgumentException("PESEL or Passport number is required");

        var nationality =
            await _dbContext.Nationalities.FirstOrDefaultAsync(n => n.Name == createRecruitmentDto.Nationality);

        if (nationality is null)
            throw new ArgumentException("Nationality not found");

        var studyProgramme =
            await _dbContext.StudyProgrammes.FirstOrDefaultAsync(sp => sp.Name == createRecruitmentDto.StudyProgramme);

        if (studyProgramme is null)
            throw new ArgumentException("Study programme not found");

        var alreadyExists = await _dbContext.Students.AnyAsync(s =>
            s.PeselNumber == createRecruitmentDto.Pesel || s.PassportNumber == createRecruitmentDto.Passport);
        if (alreadyExists)
            throw new ArgumentException("Student already exists");

        var birthdate = DateOnly.Parse(createRecruitmentDto.Birthdate);
        var age = DateTime.Now.Year - birthdate.Year;
        if (DateTime.Now.DayOfYear < birthdate.DayOfYear)
            age--;

        if (age < 18)
            throw new ArgumentException("Candidate must be at least 18 years old");

        if (createRecruitmentDto.Pesel.Length != 11)
            throw new ArgumentException("PESEL number must be 11 characters long");

        var matchTimeout = TimeSpan.FromSeconds(2);
        if (!Regex.IsMatch(createRecruitmentDto.Pesel, "[0-9]{4}[0-3]{1}[0-9}{1}[0-9]{5}", RegexOptions.None,
                matchTimeout))
            throw new ArgumentException("Invalid PESEL number");

        var status = await _dbContext.Statuses.FirstOrDefaultAsync(s => s.Name == "Candidate - registered");

        var candidate = new Student
        {
            FirstName = createRecruitmentDto.FirstName.Trim(),
            LastName = createRecruitmentDto.LastName.Trim(),
            PeselNumber = createRecruitmentDto.Pesel.Trim(),
            PassportNumber = createRecruitmentDto.Passport.Trim(),
            EmailAddress = createRecruitmentDto.Email.Trim(),
            HomeAddress = createRecruitmentDto.Address.Trim(),
            PhoneNumber = createRecruitmentDto.Phone.Trim(),
            Gender = createRecruitmentDto.Gender == "M" ? 0 : 1,
            DateOfBirth = DateOnly.Parse(createRecruitmentDto.Birthdate),
            Nationality = nationality,
            StudyProgramme = studyProgramme,
            Status = status!
        };

        //After registering we immediately setup the appointment
        var appointmentDate = await _appointmentManagerService.ScheduleAppointmentForCandidate(candidate.IdCandidate);

        var newAppointment = new Appointment
        {
            IdCandidate = candidate.IdCandidate,
            Date = appointmentDate
        };
        candidate.Appointments.Add(newAppointment);

        await _dbContext.Students.AddAsync(candidate);
        await _dbContext.SaveChangesAsync();

        return candidate;
    }

    public async Task<Appointment> GetCurrentAppointment(int idStudent)
    {
        var candidate = await _dbContext.Students.Include(c => c.Status)
            .FirstOrDefaultAsync(c => c.IdCandidate == idStudent);

        if (candidate is null)
            throw new ArgumentException("Candidate not found");

        if (candidate.Status.Name != "Candidate - registered")
            throw new ArgumentException("Candidate is not registered");

        var appointment = await _dbContext.Appointments.OrderByDescending(app => app.Date)
            .FirstOrDefaultAsync(app => app.IdCandidate == idStudent);

        return appointment;
    }

    public async Task<Appointment> CreateAppointment(int idStudent)
    {
        var candidate = await _dbContext.Students.Include(c => c.Status)
            .FirstOrDefaultAsync(c => c.IdCandidate == idStudent);

        if (candidate is null)
            throw new ArgumentException("Candidate not found");

        //Only candidate in certain statuses can schedule appointments
        if (candidate.Status.Name != "Candidate - registered" &&
            candidate.Status.Name != "Candidate - waiting for documents" &&
            candidate.Status.Name != "Candidate - waiting for signing contract")
            throw new ArgumentException("Candidate cannot schedule a meeting");

        //Cancelling previous appointments
        var previousAppointments = await _dbContext.Appointments.Include(app => app.AppointmentStatus)
            .Where(app => app.IdCandidate == idStudent).ToListAsync();

        foreach (var prevApp in previousAppointments)
            if (prevApp.AppointmentStatus.Name == "Scheduled")
                prevApp.AppointmentStatus =
                    await _dbContext.AppointmentStatuses.SingleAsync(s => s.Name == "Cancelled");

        await _appointmentManagerService.CancelAppointmentsForCandidate(idStudent);

        //Creating new appointment
        var date = await _appointmentManagerService.ScheduleAppointmentForCandidate(idStudent);

        var appointment = new Appointment
        {
            IdCandidate = idStudent,
            Date = date
        };

        //Sending the information about the new appointment to the candidate
        await _emailService.SendEmail(candidate.EmailAddress, "New appointment scheduled",
            $"You have a new appointment scheduled for {date}");

        await _dbContext.Appointments.AddAsync(appointment);
        await _dbContext.SaveChangesAsync();
        return appointment;
    }

    public async Task CancelAppointment(int idStudent)
    {
        var candidate = await _dbContext.Students.Include(c => c.Status)
            .FirstOrDefaultAsync(c => c.IdCandidate == idStudent);

        if (candidate is null)
            throw new ArgumentException("Candidate not found");

        var appointmentsToCancel = await _dbContext.Appointments.Include(app => app.AppointmentStatus)
            .Where(a => a.IdCandidate == idStudent).ToListAsync();
        foreach (var appointment in appointmentsToCancel)
            appointment.AppointmentStatus =
                await _dbContext.AppointmentStatuses.SingleAsync(s => s.Name == "Cancelled");

        //Sends the information about the cancelled appointment to the candidate
        await _emailService.SendEmail(candidate.EmailAddress, "Appointment cancelled",
            "Your appointment has been cancelled");

        await _dbContext.SaveChangesAsync();
        await _appointmentManagerService.CancelAppointmentsForCandidate(idStudent);
    }

    public async Task ConfirmDocument(int idStudent, int idDocument)
    {
        var candidate = await _dbContext.Students
            .Include(c => c.Status)
            .Include(s => s.StudyProgramme)
            .FirstOrDefaultAsync(c => c.IdCandidate == idStudent);

        if (candidate is null)
            throw new ArgumentException("Candidate not found");

        var candidatesDocument =
            await _dbContext.CandidatesDocument.FirstOrDefaultAsync(cd =>
                cd.IdDocumentType == idDocument && cd.IdCandidate == idStudent);

        if (candidatesDocument is null)
            throw new ArgumentException("Document not found");

        if (candidatesDocument.ConfirmedAt is not null)
            throw new ArgumentException("Document already confirmed");

        candidatesDocument.ConfirmedAt = DateOnly.FromDateTime(DateTime.Now);

        //Checking whether the candidates has all the required documents and
        //can be moved to the next status - waiting for signing the contract
        var requiredDocuments = await _dbContext.StudyProgrammes
            .Include(sp => sp.DocumentTypes)
            .Where(sp => sp.IdStudyProgramme == candidate.StudyProgramme.IdStudyProgramme)
            .SelectMany(sp => sp.DocumentTypes).ToListAsync();

        var confirmedDocuments = await _dbContext.CandidatesDocument
            .Include(cd => cd.DocumentType)
            .Where(cd => cd.IdCandidate == idStudent && cd.ConfirmedAt != null).Select(cd => cd.DocumentType)
            .ToListAsync();

        //Check if two lists are the same
        if (requiredDocuments.Count == confirmedDocuments.Count &&
            requiredDocuments.All(rd => confirmedDocuments.Any(cd => cd.IdDocumentType == rd.IdDocumentType)))
        {
            var status =
                await _dbContext.Statuses.SingleAsync(s =>
                    s.Name == "Candidate - waiting for signing contract");
            candidate.Status = status;
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task ConfirmAdmissionFeePayment(int idStudent)
    {
        var candidate = await _dbContext.Students.Include(c => c.Status)
            .FirstOrDefaultAsync(c => c.IdCandidate == idStudent);

        if (candidate is null)
            throw new ArgumentException("Candidate not found");

        if (candidate.Status.Name != "Candidate - waiting for payment")
            throw new ArgumentException("Candidate is not in the correct state");

        //Moving the candidate to student status
        var status = await _dbContext.Statuses.SingleAsync(s => s.Name == "Student");
        candidate.Status = status;

        //Generating new index number
        var indexNumber = await _dbContext.Students.MaxAsync(s => s.IndexNumber);
        candidate.IndexNumber = indexNumber + 1;

        await _dbContext.SaveChangesAsync();
        await _emailService.SendEmail(candidate.EmailAddress, "You have been enrolled as a student",
            "Congratulations! You have been enrolled as a student at our university. Welcome!");
    }

    public async Task CancelOngoingRecruitments()
    {
        var candidates = await _dbContext.Students
            .Include(c => c.Appointments)
            .ThenInclude(a => a.AppointmentStatus)
            .Where(s => s.Status.Name == "Candidate - registered" ||
                        s.Status.Name == "Candidate - waiting for documents" ||
                        s.Status.Name == "Candidate - waiting for signing contract" ||
                        s.Status.Name == "Candidate - waiting for payment")
            .ToListAsync();

        var cancelledStatus = await _dbContext.Statuses.SingleAsync(s => s.Name == "Candidate - cancelled");
        var cancelledStatusAppointment =
            await _dbContext.AppointmentStatuses.SingleAsync(s => s.Name == "Cancelled");

        foreach (var candidate in candidates)
        {
            candidate.Status = cancelledStatus;

            //Cancelling scheduled appointments
            var scheduledAppointments = candidate.Appointments
                .Where(app => app.AppointmentStatus.Name == "Scheduled").ToList();

            foreach (var appointment in scheduledAppointments)
                appointment.AppointmentStatus = cancelledStatusAppointment;

            await _appointmentManagerService.CancelAppointmentsForCandidate(candidate.IdCandidate);
        }

        await _dbContext.SaveChangesAsync();
    }
}