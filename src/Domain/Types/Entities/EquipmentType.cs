using System;
using BikeService.Domain.Common;

namespace BikeService.Domain.Types.Entities;

public class EquipmentType : BaseEntity
{
    public string Name { get; set; }
    public Category Category { get; set; }
    public Guid UserId { get; set; }

    public EquipmentTypeResponse CreateDto()
    {
        return new EquipmentTypeResponse(Id, Name, Category);
    }

    public EquipmentType(string name, Category category, Guid userId)
    {
        Name = name;
        Category = category;
        UserId = userId;
    }
}