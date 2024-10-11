using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Finances;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetFutureSpendingForecast;

internal sealed class GetFutureSpendingForecastQueryHandler : IQueryHandler<GetFutureSpendingForecastQuery, SpendingForecastResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    public GetFutureSpendingForecastQueryHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<SpendingForecastResponse>> Handle(GetFutureSpendingForecastQuery query, CancellationToken cancellationToken)
    {
        var pastTransactions = await _context.Transactions
            .Where(t => t.UserId == query.UserId)
            .ToListAsync(cancellationToken);
        var estimatedTotalSpending = CalculateFutureSpending(pastTransactions);
        var categoryForecasts = GenerateCategoryForecasts(pastTransactions);
        var forecastResponse = new SpendingForecastResponse
        {
            UserId = query.UserId,
            EstimatedTotalSpending = estimatedTotalSpending,
            CategoryForecasts = categoryForecasts,
            ForecastDate = _dateTimeProvider.UtcNow.AddMonths(1)
        };
        return forecastResponse;
    }
    private decimal CalculateFutureSpending(IEnumerable<Domain.Finances.Transaction>  transactions)
    {
        return transactions.Sum(t => t.Amount) * 1.1m;
    }
    private Dictionary<string, decimal> GenerateCategoryForecasts(IEnumerable<Domain.Finances.Transaction> transactions)
    {
        return transactions
         .GroupBy(t => t.Category)
         .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount) * 1.1m);
    }
}