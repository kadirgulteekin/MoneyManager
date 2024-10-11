namespace Application.Transaction.GetTransactions;

public sealed class TransactionResponse
{
    public Guid TransactionId { get; set; }
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsIncome { get; set; }
}