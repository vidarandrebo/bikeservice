using System;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Domain.Types;
using BikeService.Domain.Types.Entities;
using FluentResults;

namespace BikeService.Application;

public interface ITypeRepository
{
    Task<Result> AddType(string name, Category category, Guid userId, CancellationToken ct);
    Task<Result> DeleteType(Guid id, Guid userId, CancellationToken ct);
    Task<Result> AddDefaultTypes(Guid userId, CancellationToken ct);
    Task<Result<EquipmentTypeResponse[]>> GetTypes(Guid userId, CancellationToken ct);
}