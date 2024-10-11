using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users;

public sealed record UserCreatedDomainEvent(Guid ItemId) : IDomainEvent;
