using Domain;
using Domain.Auth;
using MediatR;

namespace Application.Interfaces;

public interface IIdentityService
{
    public Task<DataResponse<UserData>> LoginUser(string userName, string password);
    public Task<Unit> LogoutUser();
    public Task<SuccessResponse> RegisterUser(string userName, string password);
}