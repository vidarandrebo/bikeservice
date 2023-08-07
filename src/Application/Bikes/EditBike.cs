using Application.Interfaces;
using Domain;
using Domain.Bikes;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bikes;

public class EditBike
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
            var bikeId = GuidHelper.GuidOrEmpty(request.BikeFormDto.Id);
            var typeId = GuidHelper.GuidOrEmpty(request.BikeFormDto.TypeId);
            var bike = await _dbContext.Bikes
                .Where(b => b.UserId == request.UserId)
                .FirstOrDefaultAsync(b => b.Id == bikeId, cancellationToken);
            if (bike is null)
            {
                return Result.Fail(new Error("Bike not found"));
            }

            bike.Manufacturer = request.BikeFormDto.Manufacturer;
            bike.Model = request.BikeFormDto.Model;
            bike.Mileage = request.BikeFormDto.Mileage;
            bike.Date = request.BikeFormDto.Date;
            bike.TypeId = typeId;
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}