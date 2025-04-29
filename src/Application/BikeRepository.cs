using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Domain.Bikes.Contracts;
using BikeService.Domain.Bikes.Dtos;
using BikeService.Domain.Bikes.Entities;
using BikeService.Domain.Bikes.Events;
using BikeService.Domain.Common;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BikeService.Application;

public class BikeRepository : IBikeRepository
{
    private readonly IApplicationDbContext _db;
    private readonly ILogger<BikeRepository> _logger;

    public BikeRepository(IApplicationDbContext db, ILogger<BikeRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Result> AddBike(PostBikeRequest request, Guid userId, CancellationToken ct)
    {
        var bike = new Bike(request.Manufacturer, request.Model,
            request.Mileage, request.Date.ToUniversalTime(),
            GuidHelper.GuidOrEmpty(request.TypeId), userId);
        _db.Bikes.Add(bike);
        await _db.SaveChangesAsync(ct);
        return Result.Ok();
    }

    public async Task<Result> DeleteBike(Guid id, Guid userId, CancellationToken ct)
    {
        var errors = new List<Error>();
        var bike = await _db.Bikes
            .Where(b => b.UserId == userId)
            .FirstOrDefaultAsync(b => b.Id == id, ct);
        if (bike is not null)
        {
            _db.Bikes.Remove(bike);
        }
        else
        {
            errors.Add(new Error("Bike not found"));
        }

        await _db.SaveChangesAsync(ct);
        if (errors.Count > 0)
        {
            return Result.Fail(errors);
        }

        return Result.Ok();
    }

    public async Task<Result> EditBike(PutBikeRequest request, Guid userId, CancellationToken ct)
    {
        var bikeId = GuidHelper.GuidOrEmpty(request.Id);
        var typeId = GuidHelper.GuidOrEmpty(request.TypeId);
        var bike = await _db.Bikes
            .Include(b => b.ServiceNotes)
            .Where(b => b.UserId == userId)
            .FirstOrDefaultAsync(b => b.Id == bikeId, ct);
        if (bike is null)
        {
            return Result.Fail(new Error("Bike not found"));
        }

        var oldDate = bike.Date;
        bike.Manufacturer = request.Manufacturer;
        bike.Model = request.Model;
        var oldValue = bike.Mileage;
        UpdateBikeMileage(bike, request.Mileage);

        bike.Mileage = request.Mileage;
        bike.Date = request.Date.ToUniversalTime();
        bike.TypeId = typeId;

        await _db.SaveChangesAsync(ct);
        return Result.Ok();
    }
    private void UpdateBikeMileage(Bike bike, double newMileage)
    {
        var oldMileage = bike.Mileage;
        
        if (Math.Abs(newMileage - oldMileage) > 1.0)
        {
            bike.Mileage = newMileage;
            bike.AddDomainEvent(new BikeMileageUpdatedEvent(oldMileage, newMileage, bike.Id));
            var note = new ServiceNote(oldMileage, DateTime.UtcNow,
                $"Bike mileage updated from {oldMileage} to {newMileage}");

            bike.ServiceNotes.Add(note);
            _db.ServiceNotes.Add(note);
        }
    }

    public async Task<Result<BikeDto[]>> GetBikes(Guid userId, CancellationToken ct)
    {
        var bikes = await _db.Bikes
            .Where(b => b.UserId == userId)
            .Select(b => b.CreateDto())
            .ToArrayAsync(ct);

        return Result.Ok(bikes);
    }
}