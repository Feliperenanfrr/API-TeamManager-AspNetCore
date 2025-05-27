using TeamManager.Domain.Common;
using TeamManager.Domain.Enum;

namespace TeamManager.Domain.Model;

public class Workout : BaseEntity
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TypeWorkout TypeWorkou { get; set; }
    public int QuantityAthletes { get; set; }
}
