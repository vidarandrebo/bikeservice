using Application.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Parts;

public class DeletePart
{
    public record Request(Guid PartId, Guid UserId) : IRequest<Result>;


    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var part = await _dbContext.Parts
                .Where(p => p.UserId == request.UserId)
                .FirstOrDefaultAsync(p => p.Id == request.PartId, cancellationToken);
            if (part is not null)
            {
                _dbContext.Parts.Remove(part);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return Result.Ok();
            }

            return Result.Fail(new Error("Part not found"));
        }
    }
}