using BikeHistory.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikeHistory.Models.Bikes.Pipelines;

public class GetBikes
{
    public record Request() : IRequest<BikeResponse>;

    public class Handler : IRequestHandler<Request, BikeResponse>
    {
        private readonly BikeContext _bikeContext;

        public Handler(BikeContext bikeContext)
        {
            _bikeContext = bikeContext;
        }

        public async Task<BikeResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var bikes = await _bikeContext.Bikes.ToArrayAsync(cancellationToken);
            return new BikeResponse(bikes, Array.Empty<string>());
        }
    }
}
