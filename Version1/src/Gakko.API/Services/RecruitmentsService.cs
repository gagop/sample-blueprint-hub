using Gakko.API.Context;
using Gakko.API.DTOs;
using Gakko.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Gakko.API.Services;

public class RecruitmentsService : IRecruitmentsService
{
    private readonly GakkoContext _dbContext;

    public RecruitmentsService(GakkoContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Student> CreateRecruitment(CreateRecruitmentDto createRecruitmentDto)
    {
        if (string.IsNullOrWhiteSpace(createRecruitmentDto.Pesel) &&
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

        var status = await _dbContext.Statuses.FirstOrDefaultAsync(s => s.Name == "Candidate - registered");

        var candidate = new Student
        {
            Firstname = createRecruitmentDto.FirstName,
            Lastname = createRecruitmentDto.LastName,
            PeselNumber = createRecruitmentDto.Pesel,
            PassportNumber = createRecruitmentDto.Passport,
            EmailAddress = createRecruitmentDto.Email,
            HomeAddress = createRecruitmentDto.Address,
            PhoneNumber = createRecruitmentDto.Phone,
            Gender = createRecruitmentDto.Gender == "M" ? 0 : 1,
            DateOfBirth = DateOnly.Parse(createRecruitmentDto.Birthdate),
            NationalityNavigation = nationality,
            StudyProgrammeNavigation = studyProgramme,
            StatusNavigation = status!
        };

        await _dbContext.Students.AddAsync(candidate);
        await _dbContext.SaveChangesAsync();

        return candidate;
    }
}