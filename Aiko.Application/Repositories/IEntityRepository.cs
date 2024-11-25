using Aiko.Domain;
using FluentResults;

namespace Aiko.Application.Repositories;

public interface IEntityRepository
{
    Task<List<Entity>> Get(CancellationToken token);
    
    Task<Result<Entity>> TryGet(long id, CancellationToken token);
    
    Task<List<Entity>> Update(List<Entity> entity, CancellationToken token);
    
    Task<Result<string>> Remove(long id, CancellationToken token);
}