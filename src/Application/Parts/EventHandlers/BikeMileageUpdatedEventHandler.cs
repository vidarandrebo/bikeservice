using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Domain.Bikes.Events;
using BikeService.EventBus;
using Microsoft.Extensions.Logging;

namespace BikeService.Application.Parts.EventHandlers;

public class BikeMileageUpdatedEventHandler : IEventHandler
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<BikeMileageUpdatedEventHandler> _logger;

    public BikeMileageUpdatedEventHandler(IApplicationDbContext dbContext, ILogger<BikeMileageUpdatedEventHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public void Run(BaseEvent baseEvent)
    {
        throw new NotImplementedException();
    }

    public async Task RunAsync(BaseEvent baseEvent, CancellationToken ct)
    {
        BikeMileageUpdatedEvent e = (BikeMileageUpdatedEvent)baseEvent;
        var mileageDifference = e.NewMileage - e.OldMileage;
       // var partsToUpdate = _dbContext.Parts
       //     .Where(p => p.BikeId == e.BikeId);
       // foreach (var part in partsToUpdate)
       // {
       //     var oldMileage = part.Mileage;
       //     part.Mileage += mileageDifference;
       //     _logger.LogInformation("Update mileage of part {part} from {old}// to {new}", part.Manufacturer + " " + part.Model, oldMileage, part.Mileage);
        //}

        //await _dbContext.SaveChangesAsync(ct);
    }
}