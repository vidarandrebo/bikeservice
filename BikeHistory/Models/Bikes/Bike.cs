namespace BikeHistory.Models.Bikes;

public class Bike
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public double Mileage { get; set; }
    public Guid TypeId { get; set; }
    public List<ServiceEntry> Services {get;set;}

    public BikeDto CreateDto()
    {
        return new BikeDto(Id, Manufacturer, Model, Mileage, TypeId);
    }

    public Bike(string manufacturer, string model, double mileage)
    {
        Id = Guid.NewGuid();
        Manufacturer = manufacturer;
        Model = model;
        Mileage = mileage;
        Services = new List<ServiceEntry>();
    }
}
