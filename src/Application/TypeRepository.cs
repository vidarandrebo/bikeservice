using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Domain.Types;
using BikeService.Domain.Types.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BikeService.Application;

public class TypeRepository : ITypeRepository
{
    private readonly IApplicationDbContext _db;
    private readonly ILogger<TypeRepository> _logger;
    private readonly IConfiguration _configuration;

    public TypeRepository(IApplicationDbContext db, ILogger<TypeRepository> logger, IConfiguration configuration)
    {
        _db = db;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<Result<EquipmentTypeResponse[]>> GetTypes(Guid userId, CancellationToken ct)
    {
        var equipmentTypes = await _db.EquipmentTypes
            .Where(e => e.UserId == userId)
            .Select(e => e.CreateDto())
            .ToArrayAsync(ct);
        return Result.Ok(equipmentTypes);
    }

    public async Task<Result> AddType(string name, Category category, Guid userId, CancellationToken ct)
    {
        var equipmentType = new EquipmentType(name, category, userId);
        _db.EquipmentTypes.Add(equipmentType);
        await _db.SaveChangesAsync(ct);
        return Result.Ok();
    }

    public async Task<Result> DeleteType(Guid id, Guid userId, CancellationToken ct)
    {
        var equipmentType =
            await _db.EquipmentTypes
                .Where(e => e.UserId == userId)
                .FirstOrDefaultAsync(e => e.Id == id, ct);
        if (equipmentType is not null)
        {
            _db.EquipmentTypes.Remove(equipmentType);
            await _db.SaveChangesAsync(ct);
            return Result.Ok();
        }

        return Result.Fail(new Error("Type not found"));
    }

    public async Task<Result> AddDefaultTypes(Guid userId, CancellationToken ct)
    {
        var partTypes = _configuration.GetSection("DefaultPartTypes").GetArray();
        foreach (var type in partTypes)
        {
            var equipmentType = new EquipmentType(type, Category.Part, userId);
            _db.EquipmentTypes.Add(equipmentType);
        }

        var bikeTypes = _configuration.GetSection("DefaultBikeTypes").GetArray();
        foreach (var type in bikeTypes)
        {
            var equipmentType = new EquipmentType(type, Category.Bike, userId);
            _db.EquipmentTypes.Add(equipmentType);
        }

        await _db.SaveChangesAsync(ct);
        return Result.Ok();
    }
}