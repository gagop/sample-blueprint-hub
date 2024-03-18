using System;
using System.Collections.Generic;

namespace Gakko.API.Models;

public partial class Student
{
    public int Idcandidate { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public string Emailaddress { get; set; } = null!;

    public string Homeaddress { get; set; } = null!;

    public int Gender { get; set; }

    public string? Peselnumber { get; set; }

    public string? Passportnumber { get; set; }

    public DateOnly Dateofbirth { get; set; }

    public int Idnationality { get; set; }

    public int Idstudyprogramme { get; set; }

    public int Idstatus { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Nationality IdnationalityNavigation { get; set; } = null!;

    public virtual Status IdstatusNavigation { get; set; } = null!;

    public virtual Studyprogrammer IdstudyprogrammeNavigation { get; set; } = null!;
}
