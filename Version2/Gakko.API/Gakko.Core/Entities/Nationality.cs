namespace Gakko.Core.Entities;

public partial class Nationality
{
    public int IdNationality { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
