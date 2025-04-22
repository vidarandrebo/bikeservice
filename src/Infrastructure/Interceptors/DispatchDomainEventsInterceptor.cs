using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Domain.Common;
using BikeService.EventBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BikeService.Infrastructure.Interceptors;

public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IEventBusClient _eventBus;
    public DispatchDomainEventsInterceptor(IEventBusClient eventBus)
    {
        _eventBus = eventBus;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var res = await base.SavingChangesAsync(eventData, result, cancellationToken);
        await DispatchDomainEventsAsync(eventData.Context, cancellationToken);

        return res;
    }

    public async Task DispatchDomainEventsAsync(DbContext? context, CancellationToken ct)
    {
        if (context == null) return;

        var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _eventBus.ExecuteAsync(domainEvent, ct);
        }
    }
    public void DispatchDomainEvents(DbContext? context)
    {
        if (context == null) return;

        var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            _eventBus.Execute(domainEvent);
        }
    }
}