namespace BikeHistory.Models.Bike;

public class Bike
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public double Mileage { get; set; }
    public BikeType BikeType { get; set; }

    public Bike(string manufacturer, string model, double mileage)
    {
        Id = Guid.NewGuid();
        Manufacturer = manufacturer;
        Model = model;
        Mileage = mileage;
    }
}