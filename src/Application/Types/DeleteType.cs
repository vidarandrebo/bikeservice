using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Types;

public class DeleteType
{
    public record Request(Guid Id, Guid UserId) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var equipmentType =
                await _dbContext.EquipmentTypes
                    .Where(e => e.UserId == request.UserId)
                    .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (equipmentType is not null)
            {
                _dbContext.EquipmentTypes.Remove(equipmentType);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new SuccessResponse(true, Array.Empty<string>());
            }

            return new SuccessResponse(false, new[] { "Type not found" });
        }
    }
}