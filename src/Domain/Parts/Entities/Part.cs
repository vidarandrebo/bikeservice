using System;
using System.Collections.Generic;
using BikeService.Domain.Common;
using BikeService.Domain.Parts.Contracts;

namespace BikeService.Domain.Parts.Entities;

public class Part : BaseEntity
{
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public double Mileage { get; set; }
    public Status Status { get; set; }
    public Guid TypeId { get; set; }
    public Guid BikeId { get; set; }
    public Guid UserId { get; set; }
    public ICollection<ServiceNote> ServiceNotes { get; set; }

    public PartResponse ToResponse()
    {
        return new PartResponse(Id, Manufacturer, Model, Mileage, TypeId, BikeId);
    }

    public Part(string manufacturer, string model, double mileage, Guid typeId, Guid bikeId, Guid userId)
    {
        Id = Guid.NewGuid();
        Manufacturer = manufacturer;
        Model = model;
        Mileage = mileage;
        TypeId = typeId;
        BikeId = bikeId;
        UserId = userId;
        if (bikeId != Guid.Empty)
        {
            Status = Status.Active;
        }
        else
        {
            Status = Status.Inactive;
        }

        ServiceNotes = new List<ServiceNote>();
    }
}