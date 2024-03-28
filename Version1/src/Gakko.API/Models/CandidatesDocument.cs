namespace Gakko.API.Models;

public class CandidatesDocument
{
    public int IdCandidate { get; set; }
    public int IdDocumentType { get; set; }

    public DateOnly? ConfirmedAt { get; set; }

    public virtual Student Candidate { get; set; }
    public virtual DocumentType DocumentType { get; set; }
}