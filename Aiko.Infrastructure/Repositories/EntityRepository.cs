using Aiko.Application.Repositories;
using Aiko.Domain;
using Aiko.Infrastructure.Database;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aiko.Infrastructure.Repositories;

public class EntityRepository(DatabaseContext context, ILogger<EntityRepository> logger) : IEntityRepository
{
    public Task<List<Entity>> Get(CancellationToken token)
    {
        return context.Entities.ToListAsync(token);
    }

    public async Task<List<Entity>> Update(List<Entity> entities, CancellationToken token)
    {
        var entityIds = await context.Entities
            .Select(e => e.ClientId)
            .ToHashSetAsync(token);
        
        var notExistEntities = entities.Where(e => !entityIds.Contains(e.ClientId)).ToList();
        
        await context.Entities.AddRangeAsync(notExistEntities, token);
        await context.SaveChangesAsync(token);
        
        logger.LogInformation("Success updated entities");

        return entities
            .Where(e => entityIds.Contains(e.ClientId))
            .ToList();
    }

    public async Task<Result<string>> Remove(long id, CancellationToken token)
    {
       var result = await TryGet(id, token);
       if (result.IsFailed)
       {
           return Result.Fail(result.Errors.First().Message);
       }
       
       context.Entities.Remove(result.Value);
       await context.SaveChangesAsync(token);
       
       var entity = result.Value;
       logger.LogInformation("Success remove entity {@entity}",entity);
       
       return Result.Ok("Success remove entity");
    }

    public async Task<Result<Entity>> TryGet(long id, CancellationToken token)
    {
        var entity = await context.Entities.FirstOrDefaultAsync(e => e.ClientId == id, token);
        if (entity is null)
        {
            var msg = $"Not found entity with id: {id}";
            logger.LogWarning(msg);
            return Result.Fail(msg);
        }

        return Result.Ok(entity);
    }
}