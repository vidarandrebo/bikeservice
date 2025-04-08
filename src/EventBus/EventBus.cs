using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace BikeService.EventBus;

public interface IEventBus
{
    internal ChannelReader<IEvent> GetChannelReader();
}

public class EventBus : IEventBus
{
    private readonly ILogger<EventBus> _logger;
    private readonly Channel<IEvent> _channel;

    public EventBus(ILogger<EventBus> logger)
    {
        _logger = logger;
        _channel = Channel.CreateUnbounded<IEvent>();
    }

    public ChannelReader<IEvent> GetChannelReader()
    {
        return _channel.Reader;
    }

    public void Execute(IEvent e)
    {
        
    }

    public async Task Publish(IEvent e)
    {
        await _channel.Writer.WriteAsync(e);
    }
}

public interface IEvent
{
}