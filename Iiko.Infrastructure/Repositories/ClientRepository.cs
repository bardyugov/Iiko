using FluentResults;
using Iiko.Application.Commands;
using Iiko.Application.Repositories;
using Iiko.Domain;
using Iiko.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Iiko.Infrastructure.Repositories;

public class ClientRepository(DatabaseContext context, ILogger<ClientRepository> logger) : IClientRepository
{
    private Client MapInsertCommandToDomain(InsertClientCommand command)
    {
        return new Client(command.ClientId, command.Username, Guid.NewGuid());
    }
    
    public Task<List<Client>> Get(CancellationToken token)
    {
        return context.Clients.ToListAsync(token);
    }

    public async Task<List<Client>> Update(List<InsertClientCommand> clients, CancellationToken token)
    {
        var notAddedClients = new List<Client>();

        var tasks = clients.Select(async insertClient =>
        {
            var foundClient = await context.Clients.FirstOrDefaultAsync(e => e.ClientId == insertClient.ClientId, token);
            if (foundClient != null)
            {
                notAddedClients.Add(foundClient);
            }
            else
            {
                var newClient = MapInsertCommandToDomain(insertClient);
                await context.Clients.AddAsync(newClient, token);
            }
        });
        
        await Task.WhenAll(tasks);
        await context.SaveChangesAsync(token);
        
        logger.LogInformation("Success updated clients");
        return notAddedClients;
    }

    public async Task<Result<string>> Remove(long id, CancellationToken token)
    {
       var result = await TryGet(id, token);
       if (result.IsFailed)
       {
           return Result.Fail(result.Errors.First().Message);
       }
       
       context.Clients.Remove(result.Value);
       await context.SaveChangesAsync(token);
       
       var client = result.Value;
       logger.LogInformation("Success remove client {@client}",client);
       
       return Result.Ok("Success remove client");
    }

    public async Task<Result<Client>> TryGet(long id, CancellationToken token)
    {
        var client = await context.Clients.FirstOrDefaultAsync(e => e.ClientId == id, token);
        if (client is null)
        {
            var msg = $"Not found client with id: {id}";
            logger.LogWarning(msg);
            return Result.Fail(msg);
        }

        return Result.Ok(client);
    }
}