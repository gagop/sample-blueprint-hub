namespace Gakko.API.Models;

public class StudyMode
{
    public int IdStudyMode { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StudyProgramme> StudyProgrammes { get; set; } = new List<StudyProgramme>();
}