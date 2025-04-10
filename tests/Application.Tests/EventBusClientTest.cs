using System;
using BikeService.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace BikeService.Application.Tests;


public class Values
{
    public int A { get; set; }
    public string B { get; set; }
}
public class EventBusClientTest
{
    public class SomeEvent : BaseEvent
    {
        public string A { get; set; }
        public Values Values { get; set; }

        public SomeEvent()
        {
            A = "hello there";
            Values = new Values()
            {
                A = 1,
                B = "2",
            };
        }
    }
    public class SomeOtherEvent : BaseEvent
    {
        public string A { get; set; }
        public Values Values { get; set; }

        public SomeOtherEvent()
        {
            A = "hello there";
            Values = new Values()
            {
                A = 1,
                B = "2",
            };
        }
    }

    public class ThrowingHandler : IEventHandler
    {
        public void Run(BaseEvent e)
        {
            SomeEvent someEvent = (SomeEvent)e;
            throw new Exception("This is not a drill" + someEvent.A);
        }
    }

    public class UpdateNumberHandler : IEventHandler<SomeOtherEvent>
    {
        public void Run(SomeOtherEvent e)
        {
            e.Values.A += 1;
            e.Values.B = "Updated";
        }
    }

    private readonly IServiceProvider _sp;
    private readonly IEventBusClient _eventBusClient;

    public EventBusClientTest()
    {
        var sp = new ServiceCollection();
        sp.AddTransient<ThrowingHandler>();
        sp.AddTransient<UpdateNumberHandler>();
        _sp = sp.BuildServiceProvider();
        var eventBus = new EventBusClient(
            NullLogger<EventBusClient>.Instance,
            _sp);
        eventBus.AddEventHandler<SomeEvent, ThrowingHandler>();
        eventBus.AddEventHandler<SomeOtherEvent, UpdateNumberHandler>();
        _eventBusClient = eventBus;
    }

    [Fact]
    void RunEventTest()
    {
        var e = new SomeEvent();
        Assert.Throws<Exception>(() => _eventBusClient.Execute(e));
    }
    [Fact]
    void RunEventUpdateValuesTest()
    {
        var e = new SomeOtherEvent();
        _eventBusClient.Execute(e);
        
        Assert.Equal("Updated", e.Values.B);
        Assert.Equal(2, e.Values.A);
    }
}