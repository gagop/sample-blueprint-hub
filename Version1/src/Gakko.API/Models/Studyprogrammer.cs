using System;
using System.Collections.Generic;

namespace Gakko.API.Models;

public partial class Studyprogrammer
{
    public int Idstudyprogramme { get; set; }

    public string Name { get; set; } = null!;

    public int Idstudylevel { get; set; }

    public DateOnly Recruitmentstart { get; set; }

    public DateOnly Recruitmentend { get; set; }

    public int Idstudymode { get; set; }

    public virtual Studylevel IdstudylevelNavigation { get; set; } = null!;

    public virtual Studymode IdstudymodeNavigation { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Documenttype> Iddocumenttypes { get; set; } = new List<Documenttype>();
}
