using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Parts;

public class DeletePart
{
    public record Request(Guid PartId, Guid UserId) : IRequest<SuccessResponse>;


    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var part = await _dbContext.Parts
                .Where(p => p.UserId == request.UserId)
                .FirstOrDefaultAsync(p => p.Id == request.PartId, cancellationToken);
            if (part is not null)
            {
                _dbContext.Parts.Remove(part);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new SuccessResponse(true, Array.Empty<string>());
            }

            return new SuccessResponse(false, new[] {"Part not found"});
        }
    }
}