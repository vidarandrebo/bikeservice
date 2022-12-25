using Application.Interfaces;
using Domain;
using Domain.Bikes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bikes;

public class GetBikes
{
    public record Request(Guid UserId) : IRequest<DataResponse<BikeDto[]>>;

    public class Handler : IRequestHandler<Request, DataResponse<BikeDto[]>>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DataResponse<BikeDto[]>> Handle(Request request, CancellationToken cancellationToken)
        {
            var bikes = await _dbContext.Bikes
                .Where(b => b.UserId == request.UserId)
                .Select(b => b.CreateDto())
                .ToArrayAsync(cancellationToken);

            return new DataResponse<BikeDto[]>(bikes, Array.Empty<string>());
        }
    }
}