using FluentResults;
using Iiko.Application.Commands;
using Iiko.Application.Repositories;
using Iiko.Handlers.Clients.Dtos.Update;
using Microsoft.AspNetCore.Mvc;

namespace Iiko.Handlers.Clients;

[ApiController]
[Route("clients")]
public class ClientsHandler (IClientRepository clientRepository): ControllerBase
{
    private IActionResult ConvertToActionResult<T>(Result<T> result)
    {
        if (!result.IsFailed) return Ok(result.Value);
        
        var msg = result.Errors.First().Message;
        throw new BadHttpRequestException(msg);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken token)
    {
        var clients = await clientRepository.Get(token);

        return Ok(clients);
    }
    
    [HttpGet("find")]
    public async Task<IActionResult> Find([FromQuery(Name = "id")] long id, CancellationToken token)
    {
        var result = await clientRepository.TryGet(id, token);
        return ConvertToActionResult(result);
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> Remove([FromQuery(Name = "id")] long id, CancellationToken token)
    {
        var result = await clientRepository.Remove(id, token);
        return ConvertToActionResult(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateClientsDto dto, CancellationToken token)
    {
        var updateClientCommand = dto.Clients
            .Select(v => new InsertClientCommand(v.ClientId, v.Username))
            .ToList();
        
        var result = await clientRepository.Update(updateClientCommand, token);
        return Ok(result);
    }
}