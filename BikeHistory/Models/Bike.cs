namespace BikeHistory.Models;

public class Bike
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public List<Guid> Parts { get; set; }

    public Bike(string manufacturer, string model)
    {
        Id = Guid.NewGuid();
        Manufacturer = manufacturer;
        Model = model;
        Parts = new List<Guid>();
    }
}