using System;

namespace BikeService.Domain.Parts.Dtos;

public record PartDto(Guid Id, string Manufacturer, string Model, double Mileage, Guid TypeId, Guid BikeId);