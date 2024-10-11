﻿using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetFutureSpendingForecast;

public sealed record GetFutureSpendingForecastQuery(Guid UserId) : IQuery<SpendingForecastResponse>;
