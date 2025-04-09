using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace BikeService.EventBus;

public interface IEventBus
{
    internal ChannelReader<IEvent> GetChannelReader();
}

public interface IEventHandler
{
    
}

public class EventBus : IEventBus
{
    private readonly ILogger<EventBus> _logger;
    private readonly Channel<IEvent> _channel;
    private Dictionary<Type, IEventHandler> _requestHandlers;

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

        string s = "hello";
        var t = s.GetType();
        
    }

    public void AddEventHandler<T>()
    {
        var t = typeof(T);
    }

    public async Task Publish(IEvent e)
    {
        await _channel.Writer.WriteAsync(e);
    }
}

public interface IEvent
{
}