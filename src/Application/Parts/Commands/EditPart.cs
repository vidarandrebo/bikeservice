using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Domain;
using BikeService.Domain.Parts.Contracts;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikeService.Application.Parts.Commands;

public class EditPart
{
    public record Request(PutPartRequest PostPartRequest, Guid UserId) : IRequest<Result>;

    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var partId = GuidHelper.GuidOrEmpty(request.PostPartRequest.Id);
            var part = await _dbContext.Parts
                .Where(p => p.UserId == request.UserId)
                .FirstOrDefaultAsync(p => p.Id == partId, cancellationToken);
            if (part is null)
            {
                return Result.Fail(new Error("Part not found"));
            }

            var typeId = GuidHelper.GuidOrEmpty(request.PostPartRequest.TypeId);
            var bikeId = GuidHelper.GuidOrEmpty(request.PostPartRequest.BikeId);
            part.Manufacturer = request.PostPartRequest.Manufacturer;
            part.Model = request.PostPartRequest.Model;
            part.Mileage = request.PostPartRequest.Mileage;
            part.BikeId = bikeId;
            part.TypeId = typeId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}