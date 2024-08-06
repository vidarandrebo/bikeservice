using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using BikeService.Domain.Parts;
using Domain.Parts;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Parts.Commands;

public class GetParts
{
    public record Request(Guid UserId) : IRequest<Result<PartDto[]>>;

    public class Handler : IRequestHandler<Request, Result<PartDto[]>>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PartDto[]>> Handle(Request request, CancellationToken cancellationToken)
        {
            var parts = await _dbContext.Parts
                .Where(p => p.UserId == request.UserId)
                .Select(p => p.CreateDto())
                .ToArrayAsync(cancellationToken);
            return Result.Ok(parts);
        }
    }
}