using Gakko.Application.Interfaces;
using Gakko.Infrastructure.Context;
using Gakko.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gakko.Infrastructure;

public static class Configuration
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAppointmentManagerService, AppointmentManagerService>();
        services.AddScoped<IDbContext, GakkoContext>();
        return services;
    }
}