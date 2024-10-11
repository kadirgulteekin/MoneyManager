using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Transaction.GetTransactions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Reports.GetMonthlyReport;

internal sealed class GetMonthlyReportQueryHandler : IQueryHandler<GetMonthlyReportQuery, MonthlyReportResponse>
{
    private readonly IApplicationDbContext _context;

    public GetMonthlyReportQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<MonthlyReportResponse>> Handle(GetMonthlyReportQuery query, CancellationToken cancellationToken)
    {
        var transactions = await _context.Transactions
            .Where(t => t.UserId == query.UserId &&
                        t.TransactionDate.Value.Year == query.Year &&
                        t.TransactionDate.Value.Month == query.Month)
            .ToListAsync(cancellationToken);
        var totalIncome = transactions.Where(t => t.IsIncome).Sum(t => t.Amount);
        var totalExpenses = transactions.Where(t => !t.IsIncome).Sum(t => t.Amount);
        var report = new MonthlyReportResponse
        {
            UserId = query.UserId,
            Year = query.Year,
            Month = query.Month,
            TotalIncome = totalIncome,
            TotalExpenses = totalExpenses,
            Transactions = transactions.Select(t => new TransactionResponse
            {
                TransactionId = t.Id,
                AccountId = t.AccountId,
                Amount = t.Amount,
                Category = t.Category,
                Description = t.Description,
                TransactionDate = (DateTime)t.TransactionDate,
                IsIncome = t.IsIncome
            }).ToList()
        };
        return report;
    }
}