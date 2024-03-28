namespace Gakko.API.Models;

public class StudyProgramme
{
    public int IdStudyProgramme { get; set; }

    public string Name { get; set; } = null!;

    public int IdStudyLevel { get; set; }

    public DateOnly RecruitmentStart { get; set; }

    public DateOnly RecruitmentEnd { get; set; }

    public int IdStudyMode { get; set; }

    public virtual StudyLevel StudyLevel { get; set; } = null!;

    public virtual StudyMode StudyMode { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<DocumentType> DocumentTypes { get; set; } = new List<DocumentType>();
}