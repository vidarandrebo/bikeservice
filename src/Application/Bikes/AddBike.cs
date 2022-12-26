using Application.Interfaces;
using Domain;
using Domain.Bikes;
using MediatR;

namespace Application.Bikes;

public class AddBike
{
    public record Request(BikeFormDto BikeFormDto, Guid UserId) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var bike = new Bike(request.BikeFormDto.Manufacturer, request.BikeFormDto.Model,
                request.BikeFormDto.Mileage, request.BikeFormDto.Date,
                GuidHelper.GuidOrEmpty(request.BikeFormDto.TypeId), request.UserId);
            _dbContext.Bikes.Add(bike);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new SuccessResponse(true, Array.Empty<string>());
        }
    }
}