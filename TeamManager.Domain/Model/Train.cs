using TeamManager.Domain.Common;
using TeamManager.Domain.Enum;

namespace TeamManager.Domain.Model;

public class Train : BaseEntity
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TypeTrain TypeTrain { get; set; }
    public int QuantityAthletes { get; set; }
}
