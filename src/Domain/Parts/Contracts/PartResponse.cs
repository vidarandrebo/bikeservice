using System;

namespace BikeService.Domain.Parts.Contracts;

public record PartResponse(Guid Id, string Manufacturer, string Model, double Mileage, Guid TypeId, Guid BikeId);