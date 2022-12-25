namespace BikeHistory.Models.Bikes;

public class Bike
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public double Mileage { get; set; }
    public DateTime Date { get; set; }
    public Guid TypeId { get; set; }
    public List<ServiceEntry> Services { get; set; }
    public Guid UserId { get; set; }

    public BikeDto CreateDto()
    {
        return new BikeDto(Id, Manufacturer, Model, Mileage, Date, TypeId);
    }

    public Bike(string manufacturer, string model, double mileage, DateTime date, Guid typeId, Guid userId)
    {
        Id = Guid.NewGuid();
        Manufacturer = manufacturer;
        Model = model;
        Mileage = mileage;
        Date = date;
        TypeId = typeId;
        Services = new List<ServiceEntry>();
        UserId = userId;
    }
}