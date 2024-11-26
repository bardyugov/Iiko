namespace Iiko.Common.ExceptionsFilter;

public static class ExceptionsFilterExtension
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionsFilterHandler>();
    }
}