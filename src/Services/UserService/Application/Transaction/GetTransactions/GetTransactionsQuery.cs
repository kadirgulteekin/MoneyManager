using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transaction.GetTransactions;

public sealed record GetTransactionsQuery(Guid UserId, DateTime StartDate, DateTime EndDate) : IQuery<TransactionResponse>;