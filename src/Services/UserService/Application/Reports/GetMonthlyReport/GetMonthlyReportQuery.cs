using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reports.GetMonthlyReport;

public sealed record GetMonthlyReportQuery(Guid UserId, int Year, int Month) : IQuery<MonthlyReportResponse>;