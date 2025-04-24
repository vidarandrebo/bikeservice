using System;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Domain.Bikes.Contracts;
using BikeService.Domain.Bikes.Dtos;
using FluentResults;

namespace BikeService.Application;

public interface IBikeRepository
{
    Task<Result> AddBike(PostBikeRequest postBikeRequest, Guid userId, CancellationToken ct);
    Task<Result> DeleteBike(Guid id, Guid userId, CancellationToken ct);
    Task<Result> EditBike(PutBikeRequest putBikeRequest, Guid userId, CancellationToken ct);
    Task<Result<BikeDto[]>> GetBikes(Guid userId, CancellationToken ct);
}