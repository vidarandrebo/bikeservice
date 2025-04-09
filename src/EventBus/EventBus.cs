using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BikeService.EventBus;

public interface IEventBus
{
    internal ChannelReader<object> GetChannelReader();
    /// <summary>
    /// Execute synchronously
    /// </summary>
    /// <param name="e">Event</param>
    /// <typeparam name="T">Type of event</typeparam>
    void Execute<T>(T e);
    /// <summary>
    /// Fire and forget
    /// </summary>
    /// <param name="e">Event</param>
    /// <typeparam name="T">Type of event</typeparam>
    /// <returns>Task representing the action of adding the event to the queue</returns>
    Task Publish<T>(T e);
}

public interface IEventHandler<TEvent>
{
    void Run(TEvent e);
}

public class SomeEventHandler<TEvent> : IEventHandler<TEvent>
{
    public void Run(TEvent e)
    {
        var ev = e;
    }
}
public class EventBus : IEventBus
{
    private readonly ILogger<EventBus> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly Channel<object> _channel;
    private Dictionary<Type, object> _requestHandlers;

    public EventBus(ILogger<EventBus> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _requestHandlers = new Dictionary<Type, object>();
        _channel = Channel.CreateUnbounded<object>();
    }
    
    public void AddEventHandler<T>(IEventHandler<T> handler)
    {
        var t = typeof(T);
        _requestHandlers.Add(t, handler);
    }

    public void AddEventHandler<TEvent, THandler>()
    {
        var tEvent = typeof(TEvent);
        var tHandler = typeof(THandler);

        var handler = ActivatorUtilities.CreateInstance<THandler>(_serviceProvider);
        
        _requestHandlers.Add(tEvent, handler);
    }

    public ChannelReader<object> GetChannelReader()
    {
        return _channel.Reader;
    }

    public void Execute<T>(T e)
    {
        var handler = (IEventHandler<T>)_requestHandlers[typeof(T)];
        handler.Run(e);
    }


    public async Task Publish<T>(T e)
    {
        await _channel.Writer.WriteAsync(e);
    }
}
