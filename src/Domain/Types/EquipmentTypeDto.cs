using System;

namespace BikeService.Domain.Types;

public record EquipmentTypeDto(Guid Id, string Name, Category Category);