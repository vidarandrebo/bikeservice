using System;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Domain.Types;
using FluentResults;
using MediatR;

namespace BikeService.Application.Types;

public class AddType
{
    public record Request(string Name, Category Category, Guid UserId) : IRequest<Result>;

    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var equipmentType = new EquipmentType(request.Name, request.Category, request.UserId);
            _dbContext.EquipmentTypes.Add(equipmentType);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}