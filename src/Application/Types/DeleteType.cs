using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikeService.Application.Types;

public class DeleteType
{
    public record Request(Guid Id, Guid UserId) : IRequest<Result>;

    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var equipmentType =
                await _dbContext.EquipmentTypes
                    .Where(e => e.UserId == request.UserId)
                    .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (equipmentType is not null)
            {
                _dbContext.EquipmentTypes.Remove(equipmentType);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return Result.Ok();
            }

            return Result.Fail(new Error("Type not found"));
        }
    }
}