namespace BikeHistory.Models.Types;

public class EquipmentType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public Guid UserId { get; set; }

    public EquipmentTypeDto CreateDto()
    {
        return new EquipmentTypeDto(Id, Name, Category);
    }
    public EquipmentType(string name, Category category, Guid userId)
    {
        Name = name;
        Category = category;
        UserId = userId;
    }
}