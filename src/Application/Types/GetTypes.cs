﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Domain.Types;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BikeService.Application.Types;

public class GetTypes
{
    public record Request(Guid UserId) : IRequest<Result<EquipmentTypeResponse[]>>;

    public class Handler : IRequestHandler<Request, Result<EquipmentTypeResponse[]>>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<EquipmentTypeResponse[]>> Handle(Request request, CancellationToken cancellationToken)
        {
            var equipmentTypes = await _dbContext.EquipmentTypes
                .Where(e => e.UserId == request.UserId)
                .Select(e => e.CreateDto())
                .ToArrayAsync(cancellationToken);
            return Result.Ok(equipmentTypes);
        }
    }
}