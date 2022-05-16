using BikeHistory.Data;
using MediatR;

namespace BikeHistory.Models.Bikes.Pipelines;

public class AddBike
{
    public record Request(BikeDTO BikeDto) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly BikeContext _bikeContext;

        public Handler(BikeContext bikeContext)
        {
            _bikeContext = bikeContext;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var bike = new Bike(request.BikeDto.Manufacturer, request.BikeDto.Model, request.BikeDto.Mileage);
            _bikeContext.Bikes.Add(bike);
            await _bikeContext.SaveChangesAsync(cancellationToken);
            return new SuccessResponse(true, Array.Empty<string>());
        }
    }
}