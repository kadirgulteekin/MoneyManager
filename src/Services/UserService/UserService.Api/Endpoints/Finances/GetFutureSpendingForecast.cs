using Application.GetFutureSpendingForecast;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Finances;

internal sealed class GetFutureSpendingForecast : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("reports/forecast", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetFutureSpendingForecastQuery(userId);
            Result<SpendingForecastResponse> result = await sender.Send(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Reports)
        .RequireAuthorization();
    }
}
