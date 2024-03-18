using System;
using System.Collections.Generic;

namespace Gakko.API.Models;

public partial class Appointment
{
    public int Idappointment { get; set; }

    public DateOnly Date { get; set; }

    public int Idappointmentstatus { get; set; }

    public int Idcandidate { get; set; }

    public virtual Appointmentstatus IdappointmentstatusNavigation { get; set; } = null!;

    public virtual Student IdcandidateNavigation { get; set; } = null!;
}
