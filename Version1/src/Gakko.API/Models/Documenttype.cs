using System;
using System.Collections.Generic;

namespace Gakko.API.Models;

public partial class Documenttype
{
    public int Iddocumenttype { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Studyprogrammer> Idstudyprogrammes { get; set; } = new List<Studyprogrammer>();
}
