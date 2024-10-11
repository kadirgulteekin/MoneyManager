using Application.Abstractions.Behaviors;
using Application.Abstractions.Messaging;

namespace Application.Finances.Create;

public sealed class CreateTransactionCommand : ICommand<Guid>, ITransactionalRequest
{
    public Guid AccountId { get; set; }

    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsIncome { get; set; }


}
