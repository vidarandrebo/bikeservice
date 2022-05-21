using BikeHistory.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikeHistory.Models.Parts.Pipelines;

public class DeletePart
{
    public record Request(Guid PartId, Guid UserId) : IRequest<SuccessResponse>;


    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly BikeContext _bikeContext;

        public Handler(BikeContext bikeContext)
        {
            _bikeContext = bikeContext;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var part = await _bikeContext.Parts.FirstOrDefaultAsync(p => p.Id == request.PartId, cancellationToken);
            if (part is not null)
            {
                _bikeContext.Parts.Remove(part);
                await _bikeContext.SaveChangesAsync(cancellationToken);
                return new SuccessResponse(true, Array.Empty<string>());
            }

            return new SuccessResponse(false, new[] {"Part not found"});
        }
    }

}