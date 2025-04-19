using TeamManager.Enum;

namespace TeamManager.Model;

public class Train
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TypeTrain TypeTrain { get; set; }
    public int QuantityAthletes { get; set; }
}
