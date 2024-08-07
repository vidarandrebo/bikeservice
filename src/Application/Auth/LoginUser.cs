using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Domain.Auth;
using FluentResults;
using MediatR;

namespace Application.Auth;

public class LoginUser
{
    public record Request(Credentials Credentials) : IRequest<Result<UserData>>;

    public class Handler : IRequestHandler<Request, Result<UserData>>
    {
        private readonly IIdentityService _identityService;


        public Handler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<UserData>> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _identityService.LoginUser(request.Credentials.UserName, request.Credentials.Password);
        }
    }
}