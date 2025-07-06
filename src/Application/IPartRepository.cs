using System;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Domain.Parts.Contracts;
using BikeService.Domain.Parts.Entities;
using FluentResults;

namespace BikeService.Application;

public interface IPartRepository
{
    Task<Result<Part>> AddPart(PostPartRequest postPartRequest, Guid userId, CancellationToken ct);
    Task<Result> DeletePart(Guid id, Guid userId, CancellationToken ct);
    Task<Result<Part>> EditPart(PutPartRequest putPartRequest, Guid userId, CancellationToken ct);
    Task<Result<Part[]>> GetParts(Guid userId, CancellationToken ct);
}