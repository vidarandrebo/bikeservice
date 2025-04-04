﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Domain.Bikes.Contracts;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikeService.Application.Bikes;

public class EditBike
{
    public record Request(PutBikeRequest PutBikeRequest, Guid UserId) : IRequest<Result>;

    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var bikeId = GuidHelper.GuidOrEmpty(request.PutBikeRequest.Id);
            var typeId = GuidHelper.GuidOrEmpty(request.PutBikeRequest.TypeId);
            var bike = await _dbContext.Bikes
                .Where(b => b.UserId == request.UserId)
                .FirstOrDefaultAsync(b => b.Id == bikeId, cancellationToken);
            if (bike is null)
            {
                return Result.Fail(new Error("Bike not found"));
            }

            bike.Manufacturer = request.PutBikeRequest.Manufacturer;
            bike.Model = request.PutBikeRequest.Model;
            bike.UpdateMileage(request.PutBikeRequest.Mileage);
            bike.Date = request.PutBikeRequest.Date;
            bike.TypeId = typeId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}