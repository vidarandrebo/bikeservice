using System;

namespace Domain.Bikes;

public record BikeFormDto(string Id, double Mileage, string Model, string Manufacturer, DateTime Date, string TypeId);