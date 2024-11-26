using Aiko.Handlers.Clients.Commands.Create;
using FluentValidation;

namespace Aiko.Handlers.Clients.Dtos.Update;

public class UpdateClientsDtoValidator : AbstractValidator<UpdateClientsDto>
{
    
    public UpdateClientsDtoValidator()
    {
        RuleFor(cmd => cmd.Clients)
            .NotNull()
            .WithMessage("Clients field is required.")
            .Must(entities => entities?.Count > 0)
            .WithMessage("Count of clients must be greater than 10.");
        
        RuleForEach(cmd => cmd.Clients).SetValidator(new CreateClientDtoValidator());
    }
}