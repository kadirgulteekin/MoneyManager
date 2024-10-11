using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetFutureSpendingForecast;

public sealed class SpendingForecastResponse
{
    public Guid UserId { get; set; }
    public decimal EstimatedTotalSpending { get; set; }
    public Dictionary<string, decimal> CategoryForecasts { get; set; } = new();
    public DateTime ForecastDate { get; set; }
}