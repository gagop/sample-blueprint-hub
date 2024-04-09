using System.ComponentModel.DataAnnotations;

namespace Gakko.API.Recruitment;

public class CreateRecruitmentDto
{
    [Required] [MaxLength(200)] public string FirstName { get; set; } = null!;
    [Required] [MaxLength(200)] public string LastName { get; set; } = null!;
    [Required] [MaxLength(200)] public string Phone { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(200)]

    public string Email { get; set; } = null!;

    [Required] [MaxLength(255)] public string Address { get; set; } = null!;

    [Required] [MaxLength(100)] public string StudyProgramme { get; set; } = null!;

    [Required] [MaxLength(10)] public string Birthdate { get; set; }

    [MaxLength(11)] public string Pesel { get; set; } = null!;

    [MaxLength(9)] public string Passport { get; set; } = null!;

    [MaxLength(200)] public string Nationality { get; set; } = null!;

    [MaxLength(200)] public string Gender { get; set; } = null!;
}