using System;

namespace BikeService.Domain.Types;

public record EquipmentTypeResponse(Guid Id, string Name, Category Category);