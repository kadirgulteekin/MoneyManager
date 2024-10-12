using Application.GetFutureSpendingForecast;
using MediatR;
using SharedKernel;
using System;
using System.Security.Claims;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Finances;

internal sealed class GetFutureSpendingForecast : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("reports/forecast/{userId:guid}", async (Guid userId, ISender sender, CancellationToken cancellationToken, ClaimsPrincipal claimsPrincipal) =>
        {
            if (userId != claimsPrincipal.GetUserId())
            {
                //403 Forbidden
                return Results.Forbid();
            }
            var query = new GetFutureSpendingForecastQuery(userId);
            Result<SpendingForecastResponse> result = await sender.Send(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Reports)
        .RequireAuthorization()
        .WithTags(Tags.Finances)
        .CacheOutput(builder => builder
        .Expire(TimeSpan.FromMinutes(10))
        .Tag(Tags.Finances)
        .VaryByValue((httpcontext, _) =>
        {
            return ValueTask.FromResult(new KeyValuePair<string, string>(
                nameof(ClaimsPrincipalExtensions.GetUserId),
                httpcontext.User.GetUserId().ToString()));
        }),
        true);
    }
}
