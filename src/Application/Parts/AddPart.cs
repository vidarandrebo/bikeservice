using Application.Interfaces;
using Domain;
using Domain.Parts;
using MediatR;

namespace Application.Parts;

public class AddPart
{
    public record Request(PartFormDto PartFormDto, Guid UserId) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var part = new Part(request.PartFormDto.Manufacturer, request.PartFormDto.Model,
                request.PartFormDto.Mileage, GuidHelper.GuidOrEmpty(request.PartFormDto.TypeId),
                GuidHelper.GuidOrEmpty(request.PartFormDto.BikeId),request.UserId);
            _dbContext.Parts.Add(part);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new SuccessResponse(true, Array.Empty<string>());
        }
    }
}