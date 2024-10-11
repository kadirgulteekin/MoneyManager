using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Finances;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Transaction.GetTransactions;
internal sealed class GetTransactionsQueryHandler(IApplicationDbContext context) : IQueryHandler<GetTransactionsQuery, TransactionResponse>
{
    public async Task<Result<TransactionResponse>> Handle(GetTransactionsQuery query, CancellationToken cancellationToken)
    {
        TransactionResponse transactions = await context.Transactions
            .Where(t => t.UserId == query.UserId &&
                        t.TransactionDate >= query.StartDate &&
                        t.TransactionDate <= query.EndDate)
            .Select(t => new TransactionResponse
            {
                TransactionId = t.Id,
                AccountId = t.AccountId,
                Amount = t.Amount,
                Category = t.Category,
                Description = t.Description,
                TransactionDate = (DateTime)t.TransactionDate,
                IsIncome = t.IsIncome
            })
            .SingleOrDefaultAsync(cancellationToken);

        if(transactions is null)
        {
            return Result.Failure<TransactionResponse>(TransactionItemErrors.NotFound(query.UserId));
        }

        return transactions;
    }
}