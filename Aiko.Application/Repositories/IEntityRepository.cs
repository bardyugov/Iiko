using Aiko.Domain;

namespace Aiko.Application.Repositories;

public interface IEntityRepository
{
    Task<List<Entity>> Get(long id, CancellationToken token);
    
    Task Update(Entity entity, CancellationToken token);
    
    Task Remove(long id, CancellationToken token);
}