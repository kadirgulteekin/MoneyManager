using Application.Reports.GetMonthlyReport;
using MediatR;
using Polly;
using Polly.Registry;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Finances;

internal sealed class GetMonthlyReport : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("reports/monthly", async (Guid userId, int year, int month, ISender sender, CancellationToken cancellationToken,
            ResiliencePipelineProvider<string> pipelineProvider) =>
        {
            ResiliencePipeline<Result<MonthlyReportResponse>> pipeline =
            pipelineProvider.GetPipeline<Result<MonthlyReportResponse>>("gh-fallback");

            var query = new GetMonthlyReportQuery(userId, year, month);
            Result<MonthlyReportResponse> result = await pipeline.ExecuteAsync(async (context) =>
            {
                return await sender.Send(query, cancellationToken);
            }, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Reports)
        .RequireAuthorization();
    }
}
