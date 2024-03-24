namespace Gakko.API.Models;

public class StudyLevel
{
    public int IdStudyLevel { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StudyProgramme> StudyProgrammes { get; set; } = new List<StudyProgramme>();
}