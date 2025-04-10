using System;
using BikeService.Domain.Common;

namespace BikeService.Domain.Bikes.Events;

public class BikeMileageUpdatedEvent 
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