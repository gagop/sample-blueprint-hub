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

    public RecruitmentsService(GakkoContext dbContext, IAppointmentManagerService appointmentManagerService)
    {
        _dbContext = dbContext;
        _appointmentManagerService = appointmentManagerService;
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
            NationalityNavigation = nationality,
            StudyProgrammeNavigation = studyProgramme,
            StatusNavigation = status!
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
        var candidate = await _dbContext.Students.Include(c => c.StatusNavigation)
            .FirstOrDefaultAsync(c => c.IdCandidate == idStudent);

        if (candidate is null)
            throw new ArgumentException("Candidate not found");

        if (candidate.StatusNavigation.Name != "Candidate - registered")
            throw new ArgumentException("Candidate is not registered");

        var appointment = await _dbContext.Appointments.OrderByDescending(app => app.Date)
            .FirstOrDefaultAsync(app => app.IdCandidate == idStudent);

        return appointment;
    }

    public async Task<Appointment> CreateAppointment(int idStudent)
    {
        var candidate = await _dbContext.Students.Include(c => c.StatusNavigation)
            .FirstOrDefaultAsync(c => c.IdCandidate == idStudent);

        if (candidate is null)
            throw new ArgumentException("Candidate not found");

        //Only candidate in certain statuses can schedule a meeting
        if (candidate.StatusNavigation.Name != "Candidate - registered" &&
            candidate.StatusNavigation.Name != "Candidate - waiting for documents" &&
            candidate.StatusNavigation.Name != "Candidate - waiting for signing contract")
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

        await _dbContext.Appointments.AddAsync(appointment);
        await _dbContext.SaveChangesAsync();
        return appointment;
    }
}