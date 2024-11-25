using Aiko.Application.Repositories;
using Aiko.Handlers.Entities.Commands;
using Aiko.Handlers.Entities.Commands.UpdateEntitiesCommand;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Aiko.Handlers.Entities;

[ApiController]
[Route("entities")]
public class EntitiesHandler (IEntityRepository entityRepository): ControllerBase
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
        var entities = await entityRepository.Get(token);

        return Ok(entities);
    }
    
    [HttpGet("find")]
    public async Task<IActionResult> Find([FromQuery(Name = "id")] long id, CancellationToken token)
    {
        var result = await entityRepository.TryGet(id, token);
        return ConvertToActionResult(result);
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> Remove([FromQuery(Name = "id")] long id, CancellationToken token)
    {
        var result = await entityRepository.Remove(id, token);
        return ConvertToActionResult(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateEntitiesCommand command, CancellationToken token)
    {
        var result = await entityRepository.Update(command.Entities, token);
        return Ok(result);
    }
}