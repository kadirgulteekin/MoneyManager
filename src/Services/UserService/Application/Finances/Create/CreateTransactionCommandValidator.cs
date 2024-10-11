using FluentValidation;

namespace Application.Finances.Create;

internal sealed class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Amount).GreaterThan(0);
        RuleFor(c => c.Category).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Description).NotEmpty().MaximumLength(255);
        RuleFor(c => c.TransactionDate).LessThanOrEqualTo(DateTime.UtcNow);
    }
}

