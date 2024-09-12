namespace BikeService.Domain.Parts.Contracts;

public record PostPartRequest(string Id, double Mileage, string Manufacturer, string Model, string BikeId, string TypeId);