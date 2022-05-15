namespace BikeHistory.Models.Bike;

public class PartType
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public PartType(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}