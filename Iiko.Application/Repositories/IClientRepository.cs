using FluentResults;
using Iiko.Application.Commands;
using Iiko.Domain;

namespace Iiko.Application.Repositories;

public interface IClientRepository
{
    Task<List<Client>> Get(CancellationToken token);
    
    Task<Result<Client>> TryGet(long id, CancellationToken token);
    
    Task<List<Client>> Update(List<InsertClientCommand> clients, CancellationToken token);
    
    Task<Result<string>> Remove(long id, CancellationToken token);
}