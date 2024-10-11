using SharedKernel;

namespace Domain.Finances;
public sealed record FinanceItemCreatedDomainEvent(Guid ItemId) : IDomainEvent;

