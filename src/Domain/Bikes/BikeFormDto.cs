using System;

namespace BikeService.Domain.Bikes;

public record BikeFormDto(string Id, double Mileage, string Model, string Manufacturer, DateTime Date, string TypeId);