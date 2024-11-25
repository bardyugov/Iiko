using System.Net;
using System.Text.Json;
using FluentValidation;

namespace Aiko.Common.ExceptionsFilter;

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
        var code = HttpStatusCode.BadRequest;
        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
            
        if (string.IsNullOrEmpty(result))
        {
            result = JsonSerializer.Serialize(new { Error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}