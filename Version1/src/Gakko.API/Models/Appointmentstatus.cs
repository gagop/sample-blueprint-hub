namespace Gakko.API.Models;

public class Appointmentstatus
{
    public int IdAppointmentStatus { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}