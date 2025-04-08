using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BikeService.EventBus;

public class EventBus : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EventBus> _logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("hello there");
            await Task.Delay(1000);
        }
    }
}

public static class Test
{
    public static void Oi()
    {
        var test = new EventBus();
    }
}