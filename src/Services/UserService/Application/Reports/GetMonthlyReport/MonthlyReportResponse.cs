using Application.Transaction.GetTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reports.GetMonthlyReport;
public sealed class MonthlyReportResponse
{
    public Guid UserId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public List<TransactionResponse> Transactions { get; set; } = new();
}