using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Finances;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Finances.Create;

internal sealed class CreateTransactionCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateTransactionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(command.UserId));
        }

        var transactionItem = new Domain.Finances.Transaction
        {
            AccountId = command.AccountId,
            UserId = user.Id,
            Amount = command.Amount,
            Category = command.Category,
            Description = command.Description,
            TransactionDate = command.TransactionDate,
            CreatedAt = dateTimeProvider.UtcNow,
            IsIncome = command.IsIncome
        };

        transactionItem.Raise(new FinanceItemCreatedDomainEvent(transactionItem.Id));

        context.Transactions.Add(transactionItem);

        await context.SaveChangesAsync(cancellationToken);

        return transactionItem.Id;
    }
}