using BikeHistory.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikeHistory.Models.Bikes.Pipelines;

public class DeleteBike
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
            var errors = new List<string>();
            var bike = await _bikeContext.Bikes.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (bike is not null)
            {
                _bikeContext.Bikes.Remove(bike);
            }
            else
            {
                errors.Add("Bike not found");
            }

            await _bikeContext.SaveChangesAsync(cancellationToken);
            if (errors.Count > 0)
            {
                return new SuccessResponse(false, errors.ToArray());
            }

            return new SuccessResponse(true, errors.ToArray());
        }
    }
}