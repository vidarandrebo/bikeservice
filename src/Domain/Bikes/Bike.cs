﻿using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Events;

namespace Domain.Bikes;

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
    public List<ServiceEntry> Services { get; set; }
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
        Services = new List<ServiceEntry>();
        UserId = userId;
    }
}