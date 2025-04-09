using System;
using BikeService.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace BikeService.Application.Tests;

public class EventBusTest
{
    public class SomeEvent
    {
        public string A { get; set; }

        public SomeEvent()
        {
            A = "hello there";
        }
    }

    public class ThrowingHandler : IEventHandler<SomeEvent>
    {
        public void Run(SomeEvent e)
        {
            throw new Exception("This is not a drill" + e.A);
        }
    }

    private readonly IServiceProvider _sp;
    private readonly IEventBus _eventBus;

    public EventBusTest()
    {
        var sp = new ServiceCollection();
        sp.AddTransient<ThrowingHandler>();
        _sp = sp.BuildServiceProvider();
        var eventBus = new EventBus.EventBus(
            NullLogger<EventBus.EventBus>.Instance,
            _sp);
        eventBus.AddEventHandler<SomeEvent, ThrowingHandler>();
        _eventBus = eventBus;
    }

    [Fact]
    void RunEventTest()
    {
        var e = new SomeEvent();
        Assert.Throws<Exception>(() => _eventBus.Execute(e));
    }
}