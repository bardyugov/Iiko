using Aiko.Domain;
using FluentValidation;

namespace Aiko.Handlers.Entities.Commands.UpdateEntitiesCommand;

public class EntityValidator : AbstractValidator<Entity>
{
    public EntityValidator()
    {
        RuleFor(e => e.ClientId)
            .NotNull()
            .WithMessage("clientId is required.");

        RuleFor(e => e.Username)
            .NotNull()
            .NotEmpty()
            .WithMessage("username is required.");

        RuleFor(e => e.SystemId)
            .NotNull()
            .NotEmpty()
            .Must(v => v != Guid.Empty)
            .WithMessage("systemId is required.");
    }
}

public class UpdateEntitiesCommandValidator : AbstractValidator<UpdateEntitiesCommand>
{
    
    public UpdateEntitiesCommandValidator()
    {
        RuleFor(cmd => cmd.Entities)
            .Must(entities => entities.Count > 0)
            .WithMessage("Count of entities must be greater than 10.");
        
        RuleForEach(cmd => cmd.Entities).SetValidator(new EntityValidator());
    }
}