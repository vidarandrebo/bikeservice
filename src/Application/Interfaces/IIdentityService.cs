using System;
using System.Threading.Tasks;
using BikeService.Domain.Auth;
using FluentResults;
using MediatR;

namespace BikeService.Application.Interfaces;

public interface IIdentityService
{
    public Task<Result<UserData>> LoginUser(string userName, string password);
    public Task<Unit> LogoutUser();
    public Task<Result<Guid>> RegisterUser(string userName, string password);
}