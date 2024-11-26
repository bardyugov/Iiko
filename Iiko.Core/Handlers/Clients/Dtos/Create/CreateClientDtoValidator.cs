using FluentValidation;
using Iiko.Handlers.Clients.Dtos.Create;

namespace Iiko.Handlers.Clients.Commands.Create;

public class CreateClientDtoValidator : AbstractValidator<CreateClientDto>
{
    public CreateClientDtoValidator()
    {
        RuleFor(c => c.ClientId)
            .NotNull()
            .WithMessage("ClientId field is required.")
            .NotEmpty()
            .WithMessage("ClientId must be greater than 0.");

        RuleFor(e => e.Username)
            .NotNull()
            .WithMessage("Username field is required.")
            .NotEmpty()
            .WithMessage("Username field must be not empty.");
    }
}