using System;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Domain;
using BikeService.Domain.Parts;
using BikeService.Domain.Parts.Contracts;
using BikeService.Domain.Parts.Entities;
using FluentResults;
using MediatR;

namespace BikeService.Application.Parts.Commands;

public class AddPart
{
    public record Request(PostPartRequest PostPartRequest, Guid UserId) : IRequest<Result>;

    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var part = new Part(request.PostPartRequest.Manufacturer, request.PostPartRequest.Model,
                request.PostPartRequest.Mileage, GuidHelper.GuidOrEmpty(request.PostPartRequest.TypeId),
                GuidHelper.GuidOrEmpty(request.PostPartRequest.BikeId), request.UserId);
            _dbContext.Parts.Add(part);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}