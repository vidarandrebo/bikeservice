using BikeHistory.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikeHistory.Models.Parts.Pipelines;

public class GetParts
{
    public record Request(Guid UserId) : IRequest<DataResponse<PartDto[]>>;

    public class Handler : IRequestHandler<Request, DataResponse<PartDto[]>>
    {
        private readonly BikeContext _bikeContext;

        public Handler(BikeContext bikeContext)
        {
            _bikeContext = bikeContext;
        }

        public async Task<DataResponse<PartDto[]>> Handle(Request request, CancellationToken cancellationToken)
        {
            var parts = await _bikeContext.Parts
                .Include(p => p.Bike)
                .Select(p =>
                    new PartDto(p.Id, p.Manufacturer, p.Model, p.Mileage, p.Bike.Id))
                .ToArrayAsync(cancellationToken);
            return new DataResponse<PartDto[]>(parts, Array.Empty<string>());
        }
    }
}