namespace Gakko.API.Shared.Middlewares;

public static class MiddlewareExtensions
{
    public static void UseExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}