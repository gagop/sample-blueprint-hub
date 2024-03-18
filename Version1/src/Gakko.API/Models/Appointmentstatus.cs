using System;
using System.Collections.Generic;

namespace Gakko.API.Models;

public partial class Appointmentstatus
{
    public int Idappointmentstatus { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
