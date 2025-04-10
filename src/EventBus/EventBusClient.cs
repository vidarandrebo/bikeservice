using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BikeService.EventBus;

public interface IEventBusClient
{
    internal ChannelReader<object> GetChannelReader();
    /// <summary>
    /// Execute synchronously
    /// </summary>
    /// <param name="e">Event</param>
    /// <typeparam name="T">Type of event</typeparam>
    void Execute(BaseEvent e);
    /// <summary>
    /// Fire and forget
    /// </summary>
    /// <param name="e">Event</param>
    /// <typeparam name="T">Type of event</typeparam>
    /// <returns>Task representing the action of adding the event to the queue</returns>
    Task Publish<T>(T e);

    void AddEventHandler<TEvent, THandler>();
}

public interface IEventHandler
{
    void Run(BaseEvent e);
}

public abstract class BaseEvent
{
    
}
public class EventBusClient : IEventBusClient
{
    private readonly ILogger<EventBusClient> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly Channel<object> _channel;
    private Dictionary<Type, Type> _requestHandlers;

    public EventBusClient(ILogger<EventBusClient> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _requestHandlers = new Dictionary<Type, Type>();
        _channel = Channel.CreateUnbounded<object>();
    }
    
    public void AddEventHandler<TEvent, THandler>()
    {
        _requestHandlers.Add(typeof(TEvent),typeof(THandler) );
    }

    public ChannelReader<object> GetChannelReader()
    {
        return _channel.Reader;
    }

    public void Execute(BaseEvent e)
    {

        Type handlerType;
        try
        {
            handlerType = _requestHandlers[e.GetType()];
        }
        catch (KeyNotFoundException )
        {
            _logger.LogError("No handler type registered for event of type: {type}", e.GetType());
            return;
        }

        var handler = _serviceProvider.GetRequiredService(handlerType);
        
        IEventHandler typedHandler = (IEventHandler)handler;
        
        typedHandler.Run(e);
    }


    public async Task Publish<T>(T e)
    {
        await _channel.Writer.WriteAsync(e);
    }
}
