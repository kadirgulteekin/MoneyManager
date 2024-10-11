using Application.Reports.GetMonthlyReport;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Finances;

internal sealed class GetMonthlyReport : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("reports/monthly", async (Guid userId, int year, int month, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetMonthlyReportQuery(userId, year, month);
            Result<MonthlyReportResponse> result = await sender.Send(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Reports)
        .RequireAuthorization();
    }
}
