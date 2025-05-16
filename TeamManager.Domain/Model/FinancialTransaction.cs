namespace TeamManager.Domain.Model;

public class FinancialTransaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public bool TypeTransaction { get; set; }
    public string Reason { get; set; }
}
