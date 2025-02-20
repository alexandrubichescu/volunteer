namespace VolunteerConnect.Api.Middleware;
/// <summary>
/// Middleware extensions.
/// </summary>
public static class MiddlewareExtensions
{   
    /// <summary>
    /// Use custom exception handler.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
