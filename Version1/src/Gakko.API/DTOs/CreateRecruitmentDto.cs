namespace Gakko.API.DTOs;

public class CreateRecruitmentDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public int IdStudyProgramme { get; set; }
    public DateOnly Birthdate { get; set; }
    public string Pesel { get; set; }
    public string Passport { get; set; }
    public int IdNationality { get; set; }
    public string Gender { get; set; }
}