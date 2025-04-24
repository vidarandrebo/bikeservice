using BikeService.Application.Parts.EventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace BikeService.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBikeRepository, BikeRepository>();
        services.AddScoped<IPartRepository, PartRepository>();
        services.AddScoped<ITypeRepository, TypeRepository>();

        services.AddTransient<BikeMileageUpdatedEventHandler>();
        return services;
    }
}