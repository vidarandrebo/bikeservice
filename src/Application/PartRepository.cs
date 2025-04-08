using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Domain.Common;
using BikeService.Domain.Parts.Contracts;
using BikeService.Domain.Parts.Dtos;
using BikeService.Domain.Parts.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BikeService.Application;

public class PartRepository : IPartRepository
{
    private readonly IApplicationDbContext _db;
    private readonly ILogger<PartRepository> _logger;

    public async Task<Result> AddPart(PostPartRequest postPartRequest, Guid userId, CancellationToken ct)
    {
        var part = new Part(postPartRequest.Manufacturer, postPartRequest.Model,
            postPartRequest.Mileage, GuidHelper.GuidOrEmpty(postPartRequest.TypeId),
            GuidHelper.GuidOrEmpty(postPartRequest.BikeId), userId);
        _db.Parts.Add(part);
        await _db.SaveChangesAsync(ct);
        return Result.Ok();
    }

    public async Task<Result> DeletePart(Guid id, Guid userId, CancellationToken ct)
    {
        var part = await _db.Parts
            .Where(p => p.UserId == userId)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
        if (part is not null)
        {
            _db.Parts.Remove(part);
            await _db.SaveChangesAsync(ct);
            return Result.Ok();
        }

        return Result.Fail(new Error("Part not found"));
    }

    public async Task<Result> EditPart(PutPartRequest putPartRequest, Guid userId, CancellationToken ct)
    {
        var partId = GuidHelper.GuidOrEmpty(putPartRequest.Id);
        var part = await _db.Parts
            .Where(p => p.UserId == userId)
            .FirstOrDefaultAsync(p => p.Id == partId, ct);
        if (part is null)
        {
            return Result.Fail(new Error("Part not found"));
        }

        var typeId = GuidHelper.GuidOrEmpty(putPartRequest.TypeId);
        var bikeId = GuidHelper.GuidOrEmpty(putPartRequest.BikeId);
        part.Manufacturer = putPartRequest.Manufacturer;
        part.Model = putPartRequest.Model;
        part.Mileage = putPartRequest.Mileage;
        part.BikeId = bikeId;
        part.TypeId = typeId;

        if (part.BikeId != Guid.Empty)
        {
            part.Status = Status.Active;
        }
        else
        {
            part.Status = Status.Inactive;
        }

        await _db.SaveChangesAsync(ct);
        return Result.Ok();
    }

    public async Task<Result<PartResponse[]>> GetParts(Guid userId, CancellationToken ct)
    {
        var parts = await _db.Parts
            .Where(p => p.UserId == userId)
            .Select(p => p.CreateDto())
            .ToArrayAsync(ct);
        return Result.Ok(parts);
    }
}