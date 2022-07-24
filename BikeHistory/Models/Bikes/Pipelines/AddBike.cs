using System.Globalization;
using BikeHistory.Data;
using MediatR;

namespace BikeHistory.Models.Bikes.Pipelines;

public class AddBike
{
    public record Request(BikeFormDto BikeFormDto) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly BikeContext _bikeContext;

        public Handler(BikeContext bikeContext)
        {
            _bikeContext = bikeContext;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var bike = new Bike(request.BikeFormDto.Manufacturer, request.BikeFormDto.Model,
                request.BikeFormDto.Mileage, request.BikeFormDto.Date.ToUniversalTime() , GuidHelper.GuidOrEmpty(request.BikeFormDto.TypeId));
            _bikeContext.Bikes.Add(bike);
            await _bikeContext.SaveChangesAsync(cancellationToken);
            return new SuccessResponse(true, Array.Empty<string>());
        }
    }
}