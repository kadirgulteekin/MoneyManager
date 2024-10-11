using SharedKernel;

namespace Domain.Finances;

public sealed class Transaction : Entity
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public Guid UserId { get; set; } 
    public decimal Amount { get; set; }  
    public string Category { get; set; }  
    public string Description { get; set; }  
    public DateTime? TransactionDate { get; set; } 
    public bool IsIncome { get; set; }
    public DateTime CreatedAt { get; set; }
}
