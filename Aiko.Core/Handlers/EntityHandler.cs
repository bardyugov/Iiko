using Microsoft.AspNetCore.Mvc;

namespace Aiko.Handlers;

[ApiController]
[Route("entity")]
public class EntityHandler : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello");
    }
}