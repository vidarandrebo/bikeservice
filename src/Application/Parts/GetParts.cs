using Application.Interfaces;
using Domain;
using Domain.Parts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Parts;

public class GetParts
{
    public record Request(Guid UserId) : IRequest<DataResponse<PartDto[]>>;

    public class Handler : IRequestHandler<Request, DataResponse<PartDto[]>>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DataResponse<PartDto[]>> Handle(Request request, CancellationToken cancellationToken)
        {
            var parts = await _dbContext.Parts
                .Where(p => p.UserId == request.UserId)
                .Select(p => p.CreateDto())
                .ToArrayAsync(cancellationToken);
            return new DataResponse<PartDto[]>(parts, Array.Empty<string>());
        }
    }
}