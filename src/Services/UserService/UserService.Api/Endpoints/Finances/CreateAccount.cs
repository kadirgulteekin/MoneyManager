using Application.Account.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Finances;

internal sealed class CreateAccount : IEndpoint
{
    public sealed class Request
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("accounts", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateAccountCommand
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName

            };
            Result<Guid> result = await sender.Send(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Accounts)
        .RequireAuthorization();
    }
}
