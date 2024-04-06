namespace Gakko.Core.Entities;

public class DocumentType
{
    public int IdDocumentType { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StudyProgramme> StudyProgrammes { get; set; } = new List<StudyProgramme>();

    public virtual ICollection<CandidatesDocument> CandidatesDocuments { get; set; } = new List<CandidatesDocument>();
}