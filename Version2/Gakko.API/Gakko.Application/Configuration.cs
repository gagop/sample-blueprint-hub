using Gakko.Application.Interfaces;
using Gakko.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gakko.Application;

public static class Configuration
{

    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IRecruitmentsService, RecruitmentsService>();
        return services;
    }
    
}