namespace Gakko.API.Models;

public class DocumentType
{
    public int IdDocumentType { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StudyProgramme> StudyProgrammes { get; set; } = new List<StudyProgramme>();
}