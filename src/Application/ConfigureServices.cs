using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BikeService.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBikeRepository, BikeRepository>();
        services.AddScoped<ITypeRepository, TypeRepository>();
        return services;
    }
}