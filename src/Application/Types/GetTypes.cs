using Application.Interfaces;
using Domain;
using Domain.Types;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Types;

public class GetTypes
{
    public record Request(Guid UserId) : IRequest<DataResponse<EquipmentTypeDto[]>>;

    public class Handler : IRequestHandler<Request, DataResponse<EquipmentTypeDto[]>>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DataResponse<EquipmentTypeDto[]>> Handle(Request request, CancellationToken cancellationToken)
        {
            var equipmentTypes = await _dbContext.EquipmentTypes
                .Where(e => e.UserId == request.UserId)
                .Select(e => e.CreateDto())
                .ToArrayAsync(cancellationToken);
            return new DataResponse<EquipmentTypeDto[]>(equipmentTypes, Array.Empty<string>());
        }
    }
}