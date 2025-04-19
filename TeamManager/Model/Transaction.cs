namespace TeamManager.Model;

public class Transaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public bool TypeTransaction { get; set; }
    public string Reason { get; set; }
}
