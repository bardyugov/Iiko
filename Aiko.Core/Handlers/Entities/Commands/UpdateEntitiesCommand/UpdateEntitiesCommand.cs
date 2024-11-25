using Aiko.Domain;

namespace Aiko.Handlers.Entities.Commands.UpdateEntitiesCommand;

public class UpdateEntitiesCommand
{
    public List<Entity> Entities { get; set; }
}