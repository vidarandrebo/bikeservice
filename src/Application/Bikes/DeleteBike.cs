using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bikes;

public class DeleteBike
{
    public record Request(Guid Id, Guid UserId) : IRequest<Result>;

    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();
            var bike = await _dbContext.Bikes
                .Where(b => b.UserId == request.UserId)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (bike is not null)
            {
                _dbContext.Bikes.Remove(bike);
            }
            else
            {
                errors.Add(new Error("Bike not found"));
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            if (errors.Count > 0)
            {
                return Result.Fail(errors);
            }

            return Result.Ok();
        }
    }
}