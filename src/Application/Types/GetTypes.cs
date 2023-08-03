using Application.Interfaces;
using Domain.Types;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Types;

public class GetTypes
{
    public record Request(Guid UserId) : IRequest<Result<EquipmentTypeDto[]>>;

    public class Handler : IRequestHandler<Request, Result<EquipmentTypeDto[]>>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<EquipmentTypeDto[]>> Handle(Request request, CancellationToken cancellationToken)
        {
            var equipmentTypes = await _dbContext.EquipmentTypes
                .Where(e => e.UserId == request.UserId)
                .Select(e => e.CreateDto())
                .ToArrayAsync(cancellationToken);
            return Result.Ok(equipmentTypes);
        }
    }
}