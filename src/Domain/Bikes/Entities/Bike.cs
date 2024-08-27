using System;
using BikeService.Domain.Bikes.Dtos;
using BikeService.Domain.Bikes.Events;
using BikeService.Domain.Common;

namespace BikeService.Domain.Bikes.Entities;

public class Bike : BaseEntity
{
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    private double _mileage;

    public double Mileage
    {
        get { return _mileage; }
        set
        {
            if (Math.Abs(value - _mileage) > 1.0)
            {
                AddDomainEvent(new BikeMileageUpdatedEvent(_mileage, value, Id));
                _mileage = value;
            }
        }
    }

    public DateTime Date { get; set; }
    public Guid TypeId { get; set; }
    public Status Status { get; set; }
    public Guid UserId { get; set; }

    public BikeDto CreateDto()
    {
        return new BikeDto(Id, Manufacturer, Model, Mileage, Date, TypeId);
    }

    public Bike(string manufacturer, string model, double mileage, DateTime date, Guid typeId, Guid userId)
    {
        Id = Guid.NewGuid();
        Manufacturer = manufacturer;
        Model = model;
        _mileage = mileage;
        Date = date;
        TypeId = typeId;
        UserId = userId;
    }
}