using TeamManager.Enum;

namespace TeamManager.Model;

public class Coach
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CoachRole Role { get; set; }
    public DateTime BirthDay { get; set; }
}
