using System.Net;
using System.Text.Json;

namespace Iiko.Common.ExceptionsFilter;

public class ExceptionsFilterHandler(RequestDelegate next, ILogger<ExceptionsFilterHandler> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleException(exception, context);
        }
    }


    private Task HandleException(Exception exception, HttpContext context)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new { Error = exception.Message });

        switch (exception)
        {
            case BadHttpRequestException:
                code = HttpStatusCode.BadRequest;
                break;
            default:
                logger.LogCritical("Exception: {@exception}", exception);
                break;
        }
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        
        return context.Response.WriteAsync(result);
    }
}