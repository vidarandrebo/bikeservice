using System;
using System.Threading;
using System.Threading.Tasks;
using Application;
using BikeService.Application.Interfaces;
using BikeService.Domain.Types;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace BikeService.Application.Types;

public class AddDefaultTypes
{
    public record Request(Guid UserId) : IRequest<Result>;

    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public Handler(IApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var partTypes = _configuration.GetSection("DefaultPartTypes").GetArray();
            foreach (var type in partTypes)
            {
                var equipmentType = new EquipmentType(type, Category.Part, request.UserId);
                _dbContext.EquipmentTypes.Add(equipmentType);
            }

            var bikeTypes = _configuration.GetSection("DefaultBikeTypes").GetArray();
            foreach (var type in bikeTypes)
            {
                var equipmentType = new EquipmentType(type, Category.Bike, request.UserId);
                _dbContext.EquipmentTypes.Add(equipmentType);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}