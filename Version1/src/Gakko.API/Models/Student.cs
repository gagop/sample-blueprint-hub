namespace Gakko.API.Models;

public class Student
{
    public int IdCandidate { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string HomeAddress { get; set; } = null!;

    public int Gender { get; set; }

    public string? PeselNumber { get; set; }

    public string? PassportNumber { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public int IdNationality { get; set; }

    public int IdStudyProgramme { get; set; }

    public int IdStatus { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Nationality NationalityNavigation { get; set; } = null!;

    public virtual Status StatusNavigation { get; set; } = null!;

    public virtual StudyProgramme StudyProgrammeNavigation { get; set; } = null!;
}