using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Domain.Bikes;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikeService.Application.Bikes;

public class GetBikes
{
    public record Request(Guid UserId) : IRequest<Result<BikeDto[]>>;

    public class Handler : IRequestHandler<Request, Result<BikeDto[]>>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<BikeDto[]>> Handle(Request request, CancellationToken cancellationToken)
        {
            var bikes = await _dbContext.Bikes
                .Where(b => b.UserId == request.UserId)
                .Select(b => b.CreateDto())
                .ToArrayAsync(cancellationToken);

            return Result.Ok(bikes);
        }
    }
}