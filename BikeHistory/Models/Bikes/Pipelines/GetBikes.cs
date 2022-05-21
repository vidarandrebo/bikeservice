using BikeHistory.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikeHistory.Models.Bikes.Pipelines;

public class GetBikes
{
    public record Request(Guid UserId) : IRequest<DataResponse<BikeDto[]>>;

    public class Handler : IRequestHandler<Request, DataResponse<BikeDto[]>>
    {
        private readonly BikeContext _bikeContext;

        public Handler(BikeContext bikeContext)
        {
            _bikeContext = bikeContext;
        }

        public async Task<DataResponse<BikeDto[]>> Handle(Request request, CancellationToken cancellationToken)
        {
            var bikes = await _bikeContext.Bikes
                .Select(b => b.CreateDto())
                .ToArrayAsync(cancellationToken);

            return new DataResponse<BikeDto[]>(bikes, Array.Empty<string>());
        }
    }
}