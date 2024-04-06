namespace Gakko.Core.Entities;

public class Appointment
{
    public int IdAppointment { get; set; }

    public DateOnly Date { get; set; }

    public int IdAppointmentStatus { get; set; }

    public int IdCandidate { get; set; }

    public virtual AppointmentStatus AppointmentStatus { get; set; } = null!;

    public virtual Student CandidateNavigation { get; set; } = null!;
}