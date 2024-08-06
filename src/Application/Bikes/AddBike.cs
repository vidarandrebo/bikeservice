using System;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Domain;
using BikeService.Domain.Bikes;
using BikeService.Domain.Bikes.Contracts;
using BikeService.Domain.Bikes.Entities;
using FluentResults;
using MediatR;

namespace BikeService.Application.Bikes;

public class AddBike
{
    public record Request(BikeFormDto BikeFormDto, Guid UserId) : IRequest<Result>;

    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var bike = new Bike(request.BikeFormDto.Manufacturer, request.BikeFormDto.Model,
                request.BikeFormDto.Mileage, request.BikeFormDto.Date,
                GuidHelper.GuidOrEmpty(request.BikeFormDto.TypeId), request.UserId);
            _dbContext.Bikes.Add(bike);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}