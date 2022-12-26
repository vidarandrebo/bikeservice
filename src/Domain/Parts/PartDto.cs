namespace Domain.Parts;

public record PartDto(Guid Id, string Manufacturer, string Model, double Mileage, Guid TypeId, Guid BikeId);