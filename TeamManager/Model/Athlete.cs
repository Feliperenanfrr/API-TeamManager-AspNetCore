using TeamManager.Enum;

namespace TeamManager.Model;

public class Athlete
{
    public int Id { get; set; }
    public string Name { get; set; }    
    public DateTime BirthDay { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public Positions Position { get; set; }
}