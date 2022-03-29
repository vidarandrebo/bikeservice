namespace BikeHistory.Models;

public class Part
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }

    public Part(string manufacturer, string model)
    {
        Id = Guid.NewGuid();
        Manufacturer = manufacturer;
        Model = model;
    }
}