using System;
using System.Collections.Generic;

namespace Gakko.API.Models;

public partial class Studymode
{
    public int Idstudymode { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Studyprogrammer> Studyprogrammers { get; set; } = new List<Studyprogrammer>();
}
