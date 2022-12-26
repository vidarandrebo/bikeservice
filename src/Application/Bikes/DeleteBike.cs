using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bikes;

public class DeleteBike
{
    public record Request(Guid Id, Guid UserId) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();
            var bike = await _dbContext.Bikes
                .Where(b => b.UserId == request.UserId)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (bike is not null)
            {
                _dbContext.Bikes.Remove(bike);
            }
            else
            {
                errors.Add("Bike not found");
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            if (errors.Count > 0)
            {
                return new SuccessResponse(false, errors.ToArray());
            }

            return new SuccessResponse(true, errors.ToArray());
        }
    }
}