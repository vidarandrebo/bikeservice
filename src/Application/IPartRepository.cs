using System;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Domain.Bikes.Contracts;
using BikeService.Domain.Bikes.Dtos;
using BikeService.Domain.Parts.Contracts;
using BikeService.Domain.Parts.Dtos;
using FluentResults;

namespace BikeService.Application;

public interface IPartRepository
{
    Task<Result> AddPart(PostPartRequest postPartRequest, Guid userId, CancellationToken ct);
    Task<Result> DeletePart(Guid id, Guid userId, CancellationToken ct);
    Task<Result> EditPart(PutPartRequest putPartRequest, Guid userId, CancellationToken ct);
    Task<Result<PartResponse[]>> GetParts(Guid userId, CancellationToken ct);
}