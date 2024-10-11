using Application.Finances.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Finances;
internal sealed class CreateTransaction : IEndpoint
{
    public sealed class Request
    {
        public Guid AccountId { get; set; }

        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsIncome { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("transactions", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateTransactionCommand
            {
                AccountId = request.AccountId,
                UserId = request.UserId,
                Amount = request.Amount,
                Category = request.Category,
                Description = request.Description,
                TransactionDate = request.TransactionDate,
                IsIncome = request.IsIncome
            };
            Result<Guid> result = await sender.Send(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Transactions)
        .RequireAuthorization();
    }

}