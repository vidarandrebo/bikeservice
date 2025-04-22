using System;
using BikeService.Domain.Common;
using BaseEvent = BikeService.EventBus.BaseEvent;

namespace BikeService.Domain.Bikes.Events;

public class BikeMileageUpdatedEvent : BaseEvent
{
    public double OldMileage;
    public double NewMileage;
    public Guid BikeId;

    public BikeMileageUpdatedEvent(double oldMileage, double newMileage, Guid bikeId)
    {
        OldMileage = oldMileage;
        NewMileage = newMileage;
        BikeId = bikeId;
    }
}