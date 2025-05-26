using TeamManager.Domain.Common;
using TeamManager.Domain.Enum;

namespace TeamManager.Domain.Model;

public class Athlete : BaseEntity
{
    public string Name { get; set; }
    public DateTime BirthDay { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public Positions Position { get; set; }

    public int Age =>
        DateTime.Now.Year - BirthDay.Year - (DateTime.Now.DayOfYear < BirthDay.DayOfYear ? 1 : 0);
}
