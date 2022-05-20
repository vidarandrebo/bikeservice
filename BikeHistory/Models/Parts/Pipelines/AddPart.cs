using BikeHistory.Data;
using MediatR;

namespace BikeHistory.Models.Parts.Pipelines;

public class AddPart
{
    public record Request(PartFormDto PartFormDto) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly BikeContext _bikeContext;

        public Handler(BikeContext bikeContext)
        {
            _bikeContext = bikeContext;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var part = new Part(request.PartFormDto.Manufacturer, request.PartFormDto.Model,
                request.PartFormDto.Mileage);
            _bikeContext.Parts.Add(part);
            await _bikeContext.SaveChangesAsync(cancellationToken);
            return new SuccessResponse(true, Array.Empty<string>());
        }
    }
}