namespace BikeHistory.Models.Bike;

public class BikeType
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public BikeType(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}