using System;
using System.Collections.Generic;

namespace Gakko.API.Models;

public partial class Status
{
    public int IdStatus { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
