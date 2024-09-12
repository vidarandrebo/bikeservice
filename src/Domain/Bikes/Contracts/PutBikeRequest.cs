using System;

namespace BikeService.Domain.Bikes.Contracts;

public record PutBikeRequest(string Id, double Mileage, string Model, string Manufacturer, DateTime Date, string TypeId);