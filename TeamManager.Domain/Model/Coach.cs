using TeamManager.Domain.Common;
using TeamManager.Domain.Enum;

namespace TeamManager.Domain.Model;

public class Coach : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CoachRole Role { get; set; }
    public DateTime BirthDay { get; set; }

    public int Age =>
        DateTime.Now.Year - BirthDay.Year - (DateTime.Now.DayOfYear < BirthDay.DayOfYear ? 1 : 0);
}
