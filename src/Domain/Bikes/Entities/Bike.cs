using System;
using System.Collections.Generic;
using BikeService.Domain.Bikes.Dtos;
using BikeService.Domain.Bikes.Events;
using BikeService.Domain.Common;

namespace BikeService.Domain.Bikes.Entities;

public class Bike : BaseEntity
{
    public string Manufacturer { get; set; }
    public string Model { get; set; }

    public double Mileage { get; set; }

    public DateTime Date { get; set; }
    public Guid TypeId { get; set; }
    public Status Status { get; set; }
    public Guid UserId { get; set; }
    public List<ServiceNote> ServiceNotes { get; set; }

    public BikeDto CreateDto()
    {
        return new BikeDto(Id, Manufacturer, Model, Mileage, Date, TypeId);
    }

    public void UpdateMileage(double newValue)
    {
        var oldValue = Mileage;
        if (Math.Abs(newValue - oldValue) > 1.0)
        {
            Mileage = newValue;
            AddDomainEvent(new BikeMileageUpdatedEvent(oldValue, newValue, Id));
            ServiceNotes.AddServiceNote(DateTime.Now, oldValue,
                $"Bike mileage updated from {oldValue} to {newValue}");
        }
    }

    public Bike(string manufacturer, string model, double mileage, DateTime date, Guid typeId, Guid userId)
    {
        Id = Guid.NewGuid();
        Manufacturer = manufacturer;
        Model = model;
        Mileage = mileage;
        Date = date;
        TypeId = typeId;
        UserId = userId;
        Status = Status.Active;
        ServiceNotes = new List<ServiceNote>();
    }
}