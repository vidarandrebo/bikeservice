using Application.Interfaces;
using Domain;
using Domain.Types;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Types;

public class AddDefaultTypes
{
    public record Request(Guid UserId) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public Handler(IApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var partTypes = _configuration.GetSection("DefaultPartTypes").GetArray();
            foreach (var type in partTypes)
            {
                var equipmentType = new EquipmentType(type, Category.Part, request.UserId);
                _dbContext.EquipmentTypes.Add(equipmentType);
            }

            var bikeTypes = _configuration.GetSection("DefaultBikeTypes").GetArray();
            foreach (var type in bikeTypes)
            {
                var equipmentType = new EquipmentType(type, Category.Bike, request.UserId);
                _dbContext.EquipmentTypes.Add(equipmentType);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return new SuccessResponse(true, Array.Empty<string>());
        }
    }
}