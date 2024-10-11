using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Finances;

public class TransactionItemErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound(
       "TransactionItem.NotFound",
       $"The transaction item with the Id = '{userId}' was not found");
}
