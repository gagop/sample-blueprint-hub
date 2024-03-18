using System;
using System.Collections.Generic;

namespace Gakko.API.Models;

public partial class Studylevel
{
    public int Idstudylevel { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Studyprogrammer> Studyprogrammers { get; set; } = new List<Studyprogrammer>();
}
