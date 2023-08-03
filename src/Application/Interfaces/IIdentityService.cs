using Domain;
using Domain.Auth;
using FluentResults;
using MediatR;

namespace Application.Interfaces;

public interface IIdentityService
{
    public Task<Result<UserData>> LoginUser(string userName, string password);
    public Task<Unit> LogoutUser();
    public Task<Result<Guid>> RegisterUser(string userName, string password);
}