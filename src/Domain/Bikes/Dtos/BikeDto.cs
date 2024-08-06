using System;

namespace BikeService.Domain.Bikes;

public record BikeDto(Guid Id, string Manufacturer, string Model, double Mileage, DateTime Date, Guid TypeId);