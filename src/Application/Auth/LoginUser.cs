using Application.Interfaces;
using Domain;
using Domain.Auth;
using Infrastructure.Identity;
using MediatR;

namespace Application.Auth;

public class LoginUser
{
    public record Request(Credentials Credentials) : IRequest<DataResponse<UserData>>;

    public class Handler : IRequestHandler<Request, DataResponse<UserData>>
    {
        private readonly IIdentityService _identityService;


        public Handler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<DataResponse<UserData>> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _identityService.LoginUser(request.Credentials.UserName, request.Credentials.Password);
        }
    }
}