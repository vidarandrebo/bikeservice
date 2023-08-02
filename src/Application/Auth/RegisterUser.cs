using Application.Interfaces;
using Domain;
using Infrastructure.Identity;
using LanguageExt.Common;
using MediatR;

namespace Application.Auth;

public class RegisterUser
{
    public record Request(Credentials Credentials) : IRequest<Result<Guid>>;

    public class Handler : IRequestHandler<Request, Result<Guid>>
    {
        private readonly IIdentityService _identityService;

        public Handler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<Guid>> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _identityService.RegisterUser(request.Credentials.UserName, request.Credentials.Password);
        }
    }
}