using Gakko.API.Shared.Context;
using Gakko.API.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Gakko.API.Shared;

public static class Configuration
{
    public static void RegisterSharedServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<GakkoContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("GakkoDb")));
        services.AddScoped<IAppointmentManagerService, AppointmentManagerService>();
        services.AddScoped<IEmailService, EmailService>();
    }
}