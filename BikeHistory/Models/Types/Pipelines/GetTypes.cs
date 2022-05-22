using BikeHistory.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikeHistory.Models.Types.Pipelines;

public class GetTypes
{
    public record Request(Guid UserId) : IRequest<DataResponse<EquipmentTypeDto[]>>;

    public class Handler : IRequestHandler<Request, DataResponse<EquipmentTypeDto[]>>
    {
        private readonly BikeContext _bikeContext;

        public Handler(BikeContext bikeContext)
        {
            _bikeContext = bikeContext;
        }

        public async Task<DataResponse<EquipmentTypeDto[]>> Handle(Request request, CancellationToken cancellationToken)
        {
            var equipmentTypes = await _bikeContext.EquipmentTypes
                .Where(e => e.UserId == request.UserId)
                .Select(e => e.CreateDto())
                .ToArrayAsync(cancellationToken);
            return new DataResponse<EquipmentTypeDto[]>(equipmentTypes, Array.Empty<string>());
        }
    }
}