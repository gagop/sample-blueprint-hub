namespace Gakko.API.Middlewares;

public static class MiddlewareExtensions
{
    public static void UseExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}