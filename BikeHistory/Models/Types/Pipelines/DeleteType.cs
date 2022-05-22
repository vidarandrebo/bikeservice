using BikeHistory.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikeHistory.Models.Types.Pipelines;

public class DeleteType
{
    public record Request(Guid Id, Guid UserId) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly BikeContext _bikeContext;

        public Handler(BikeContext bikeContext)
        {
            _bikeContext = bikeContext;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var equipmentType =
                await _bikeContext.EquipmentTypes
                    .Where(e => e.UserId == request.UserId)
                    .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (equipmentType is not null)
            {
                _bikeContext.EquipmentTypes.Remove(equipmentType);
                await _bikeContext.SaveChangesAsync(cancellationToken);
                return new SuccessResponse(true, Array.Empty<string>());
            }

            return new SuccessResponse(false, new[] { "Type not found" });
        }
    }
}