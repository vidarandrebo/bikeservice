namespace BikeHistory.Models.Bikes;

public class Part
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public double Mileage { get; set; }
    public PartType? PartType { get; set; }
    public Bikes.Bike? Bike { get; set; }

    public Part(string manufacturer, string model, double mileage)
    {
        Id = Guid.NewGuid();
        Manufacturer = manufacturer;
        Model = model;
        Mileage = mileage;
    }
}