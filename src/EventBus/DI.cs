using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BikeService.EventBus;

public static class DI
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBus, EventBus>();
        services.AddHostedService<EventBusWorker>();
        return services;
    }
}