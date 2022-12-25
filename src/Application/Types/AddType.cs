using Application.Interfaces;
using Domain;
using Domain.Types;
using MediatR;

namespace Application.Types;

public class AddType
{
    public record Request(string Name, Category Category, Guid UserId) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var equipmentType = new EquipmentType(request.Name, request.Category, request.UserId);
            _dbContext.EquipmentTypes.Add(equipmentType);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new SuccessResponse(true, Array.Empty<string>());
        }
    }
}