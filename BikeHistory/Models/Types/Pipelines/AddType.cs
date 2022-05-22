using BikeHistory.Data;
using MediatR;

namespace BikeHistory.Models.Types.Pipelines;

public class AddType
{
    public record Request(string Name, Category Category, Guid UserId) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly BikeContext _bikeContext;

        public Handler(BikeContext bikeContext)
        {
            _bikeContext = bikeContext;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var equipmentType = new EquipmentType(request.Name, request.Category, request.UserId);
            _bikeContext.EquipmentTypes.Add(equipmentType);
            await _bikeContext.SaveChangesAsync(cancellationToken);
            return new SuccessResponse(true, Array.Empty<string>());
        }
    }
}