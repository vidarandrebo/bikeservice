namespace BikeHistory.Models.Parts;

public class Part
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public double Mileage { get; set; }
    public Guid TypeId { get; set; }
    public Guid BikeId { get; set; }
    public Guid UserId { get; set; }

    public PartDto CreateDto()
    {
        return new PartDto(Id, Manufacturer, Model, Mileage, TypeId, BikeId);
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
    }
}