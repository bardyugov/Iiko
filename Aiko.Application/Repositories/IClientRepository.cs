using Aiko.Application.Commands;
using Aiko.Domain;
using FluentResults;

namespace Aiko.Application.Repositories;

public interface IClientRepository
{
    Task<List<Client>> Get(CancellationToken token);
    
    Task<Result<Client>> TryGet(long id, CancellationToken token);
    
    Task<List<Client>> Update(List<InsertClientCommand> clients, CancellationToken token);
    
    Task<Result<string>> Remove(long id, CancellationToken token);
}