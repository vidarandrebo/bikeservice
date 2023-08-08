using Application.Interfaces;
using Domain.Events;
using MediatR;

namespace Application.Parts.EventHandlers;

public class BikeMileageUpdatedEventHandler : INotificationHandler<BikeMileageUpdatedEvent>
{
    private readonly IApplicationDbContext _dbContext;

    public BikeMileageUpdatedEventHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(BikeMileageUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var mileageDifference = notification.NewMileage - notification.OldMileage;
        var partsToUpdate = _dbContext.Parts
            .Where(p => p.BikeId == notification.BikeId);
        foreach (var part in partsToUpdate)
        {
            part.Mileage += mileageDifference;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}