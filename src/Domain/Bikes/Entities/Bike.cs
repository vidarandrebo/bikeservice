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
    public ICollection<ServiceNote> ServiceNotes { get; set; }

    public BikeDto CreateDto()
    {
        return new BikeDto(Id, Manufacturer, Model, Mileage, Date, TypeId);
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