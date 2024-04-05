using Gakko.API.Context;
using Gakko.API.DTOs;
using Gakko.API.Models;
using Gakko.API.Services;
using Gakko.Tests.Setup;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Gakko.Tests.RecruitmentsService;

public class RecruitmentsServiceTests
{
    private readonly GakkoContext _dbContext;
    private readonly API.Services.RecruitmentsService _service;

    public RecruitmentsServiceTests()
    {
        _dbContext = GakkoDbContextForTestsFactory.CreateDbContextForInMemory();
        var emailService = Substitute.For<IEmailService>();
        var appointmentService = Substitute.For<IAppointmentManagerService>();
        appointmentService.ScheduleAppointmentForCandidate(1).Returns(new DateOnly(2025, 1, 5));
        _service = new API.Services.RecruitmentsService(_dbContext, appointmentService, emailService);
    }

    [Fact]
    public async Task CancelOngoingRecruitmentsAsync_Should_Cancel_All_The_Ongoing_Recruitments()
    {
        //Arrange
        await _dbContext.Students.AddAsync(new Student
        {
            IdCandidate = 1,
            FirstName = "John",
            LastName = "Doe",
            HomeAddress = "Warsaw, Zlota 12",
            IdNationality = 1,
            IdStatus = 1,
            EmailAddress = "doe@gmail.com",
            PeselNumber = "50010112345",
            PhoneNumber = "454-232-232",
            DateOfBirth = new DateOnly(1970, 1, 1)
        });

        await _dbContext.Appointments.AddAsync(new Appointment
        {
            IdCandidate = 1,
            Date = new DateOnly(2025, 1, 1),
            IdAppointmentStatus = 1
        });
        await _dbContext.SaveChangesAsync();

        //Act
        await _service.CancelOngoingRecruitmentsAsync();

        //Assert
        var appointments = await _dbContext.Appointments.ToListAsync();
        Assert.True(appointments.Count() == 1);
        Assert.True(appointments.FirstOrDefault().IdAppointmentStatus == 2);
    }

    [Fact]
    public async void CreateRecruitmentAsync_Should_Return_Argument_Exception_When_Student_Is_Too_Young()
    {
        //Arrange
        var createRecruitmentDto = new CreateRecruitmentDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
            Phone = "123456789",
            Address = "123 Main St",
            StudyProgramme = "Computer Science",
            Birthdate = "2020-01-01",
            Pesel = "50010112345",
            Nationality = "Polish",
            Gender = "M"
        };

        //Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateRecruitmentAsync(createRecruitmentDto));
    }

    [Fact]
    public async void CreateRecruitmentAsync_Should_Return_New_Argument_Exception_When_Student_Already_Exists()
    {
        //Arrange
        var candidate = new Student
        {
            IdCandidate = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "doe@gmail.com",
            PhoneNumber = "123456789",
            DateOfBirth = new DateOnly(1990, 1, 1),
            HomeAddress = "123 Main St",
            IndexNumber = null,
            Gender = 0,
            PeselNumber = "50010112345",
            IdStatus = 1,
            IdNationality = 1,
            IdStudyProgramme = 1,
            Appointments = new List<Appointment>
            {
                new()
                {
                    IdAppointmentStatus = 1,
                    Date = new DateOnly(2030, 1, 1)
                }
            }
        };
        _dbContext.Students.Add(candidate);
        await _dbContext.SaveChangesAsync();

        var createRecruitmentDto = new CreateRecruitmentDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
            Phone = "123456789",
            Address = "123 Main St",
            StudyProgramme = "Computer Science",
            Birthdate = "1990-01-01",
            Pesel = "50010112345",
            Nationality = "Polish",
            Gender = "M"
        };

        //Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateRecruitmentAsync(createRecruitmentDto));
    }

    [Theory]
    [InlineData("", "Polish", "Computer Science")]
    [InlineData("5343343", "Polish", "Computer Science")]
    [InlineData("50010112345", "AA", "AA")]
    [InlineData("50010112345", "Polish", "AA")]
    [InlineData("50010112345", "AA", "Computer Science")]
    public async void CreateRecruitmentAsync_Should_Return_Argument_Exception_When_Input_Data_Is_Not_Correct(
        string pesel, string nationality, string studyProgramme)
    {
        //Arrange
        var createRecruitmentDto = new CreateRecruitmentDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
            Phone = "123456789",
            Address = "123 Main St",
            StudyProgramme = studyProgramme,
            Birthdate = "1990-01-01",
            Pesel = pesel,
            Nationality = nationality,
            Gender = "M"
        };

        //Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateRecruitmentAsync(createRecruitmentDto));
    }


    [Fact]
    public async void CreateRecruitmentAsync_Should_Return_New_Student_When_Student_Provided_All_Required_Information()
    {
        //Arrange
        var createRecruitmentDto = new CreateRecruitmentDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "doe@gmail.com",
            Phone = "123456789",
            Address = "123 Main St",
            StudyProgramme = "Computer Science",
            Birthdate = "1990-01-01",
            Pesel = "50010112345",
            Nationality = "Polish",
            Gender = "M"
        };

        //Act
        var result = await _service.CreateRecruitmentAsync(createRecruitmentDto);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IdCandidate > 0);
        Assert.True(result.FirstName == "John");
        Assert.True(result.LastName == "Doe");
        Assert.True(result.Status.Name == "Candidate - registered");
        Assert.True(result.Appointments.Count == 1);
    }

    [Fact]
    public async void
        CreateAppointmentAsync_Should_Return_New_Appointment_And_Cancel_Previous_Appointments_When_Student_Is_In_Correct_State()
    {
        //Arrange
        var candidate = new Student
        {
            IdCandidate = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "doe@gmail.com",
            PhoneNumber = "123456789",
            DateOfBirth = new DateOnly(1990, 1, 1),
            HomeAddress = "123 Main St",
            IndexNumber = null,
            Gender = 0,
            PeselNumber = "12345678901",
            IdStatus = 1,
            IdNationality = 1,
            IdStudyProgramme = 1,
            Appointments = new List<Appointment>
            {
                new()
                {
                    IdAppointmentStatus = 1,
                    Date = new DateOnly(2030, 1, 1)
                }
            }
        };
        _dbContext.Students.Add(candidate);
        await _dbContext.SaveChangesAsync();

        //Act
        var result = await _service.CreateAppointmentAsync(1);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IdAppointmentStatus == 1);
        Assert.True(result.Date == new DateOnly(2025, 1, 5));
        Assert.True(candidate.Appointments.Count(a => a.AppointmentStatus.Name == "Cancelled") == 1);
    }

    [Fact]
    public async void CreateAppointmentAsync_Should_Return_Argument_Exception_When_Student_Is_Not_In_Correct_State()
    {
        //Arrange
        var candidate = new Student
        {
            IdCandidate = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "doe@gmail.com",
            PhoneNumber = "123456789",
            DateOfBirth = new DateOnly(1990, 1, 1),
            HomeAddress = "123 Main St",
            IndexNumber = null,
            Gender = 0,
            PeselNumber = "12345678901",
            IdStatus = 5,
            IdNationality = 1,
            IdStudyProgramme = 1,
            Appointments = new List<Appointment>
            {
                new()
                {
                    IdAppointmentStatus = 1,
                    Date = new DateOnly(2030, 1, 1)
                }
            }
        };
        _dbContext.Students.Add(candidate);
        await _dbContext.SaveChangesAsync();

        //Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAppointmentAsync(1));
    }

    [Fact]
    public async void CreateAppointmentAsync_Should_Return_Argument_Exception_When_Student_Does_Not_Exist()
    {
        //Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAppointmentAsync(1));
    }

    [Fact]
    public async void CreateAppointmentAsync_Should_Return_New_Appointment_When_Student_Has_Been_Registered()
    {
        //Arrange
        var candidate = new Student
        {
            IdCandidate = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "doe@gmail.com",
            PhoneNumber = "123456789",
            DateOfBirth = new DateOnly(1990, 1, 1),
            HomeAddress = "123 Main St",
            IndexNumber = null,
            Gender = 0,
            PeselNumber = "12345678901",
            IdStatus = 1,
            IdNationality = 1,
            IdStudyProgramme = 1,
            Appointments = new List<Appointment>
            {
                new()
                {
                    IdAppointmentStatus = 1,
                    Date = new DateOnly(2030, 1, 1)
                }
            }
        };
        _dbContext.Students.Add(candidate);
        await _dbContext.SaveChangesAsync();

        //Act
        var result = await _service.CreateAppointmentAsync(1);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.AppointmentStatus.Name == "Scheduled");
        Assert.True(result.Date == new DateOnly(2025, 1, 5));
    }

    [Fact]
    public async Task GetCurrentAppointment_Should_Return_Single_Appointment_When_Student_Has_Been_Registered()
    {
        //Arrange
        var candidate = new Student
        {
            IdCandidate = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "doe@gmail.com",
            PhoneNumber = "123456789",
            DateOfBirth = new DateOnly(1990, 1, 1),
            HomeAddress = "123 Main St",
            IndexNumber = null,
            Gender = 0,
            PeselNumber = "12345678901",
            IdStatus = 1,
            IdNationality = 1,
            IdStudyProgramme = 1,
            Appointments = new List<Appointment>
            {
                new()
                {
                    IdAppointmentStatus = 1,
                    Date = new DateOnly(2030, 1, 1)
                }
            }
        };
        _dbContext.Students.Add(candidate);
        await _dbContext.SaveChangesAsync();

        //Act
        var result = await _service.GetCurrentAppointmentAsync(candidate.IdCandidate);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IdAppointmentStatus == 1);
        Assert.True(result.Date == new DateOnly(2030, 1, 1));
    }

    [Fact]
    public void GetCurrentAppointment_Should_Return_400_When_Student_Does_Not_Exist()
    {
        //Act and Assert
        Assert.ThrowsAsync<ArgumentException>(() => _service.GetCurrentAppointmentAsync(1));
    }

    [Fact]
    public void GetCurrentAppointment_Should_Return_400_When_Student_Is_In_Incorrect_State()
    {
        //Arrange
        var candidate = new Student
        {
            IdCandidate = 1,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "doe@gmail.com",
            PhoneNumber = "123456789",
            DateOfBirth = new DateOnly(1990, 1, 1),
            HomeAddress = "123 Main St",
            IndexNumber = null,
            Gender = 0,
            PeselNumber = "12345678901",
            IdStatus = 3,
            IdNationality = 1,
            IdStudyProgramme = 1,
            Appointments = new List<Appointment>
            {
                new()
                {
                    IdAppointmentStatus = 1,
                    Date = new DateOnly(2030, 1, 1)
                }
            }
        };
        _dbContext.Students.Add(candidate);
        _dbContext.SaveChanges();

        //Act and Assert
        Assert.ThrowsAsync<ArgumentException>(() => _service.GetCurrentAppointmentAsync(1));
    }
}