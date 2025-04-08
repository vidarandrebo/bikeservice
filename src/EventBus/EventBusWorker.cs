using System.Threading.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BikeService.EventBus;

public class EventBusWorker : BackgroundService
{
    
    private readonly ILogger<EventBusWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ChannelReader<IEvent> _eventChan;

    public EventBusWorker(ILogger<EventBusWorker> logger, IServiceProvider serviceProvider, IEventBus eventBus)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _eventChan = eventBus.GetChannelReader();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var data = await _eventChan.ReadAsync(stoppingToken);
            Console.WriteLine("hello there");
            await Task.Delay(1000);
        }
    }
}