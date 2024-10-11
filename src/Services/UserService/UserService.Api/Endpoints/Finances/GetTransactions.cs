using Application.Transaction.GetTransactions;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Finances;

internal sealed class GetTransactions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("transactions", async (Guid userId, DateTime startDate, DateTime endDate, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetTransactionsQuery(userId, startDate, endDate);
            Result<TransactionResponse> result = await sender.Send(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Transactions)
        .RequireAuthorization();
    }
}