using System;
using BikeService.Domain.Types.Entities;

namespace BikeService.Domain.Types;

public record EquipmentTypeResponse(Guid Id, string Name, Category Category);