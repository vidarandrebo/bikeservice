using Application.Interfaces;
using Domain;
using Infrastructure.Identity;
using MediatR;

namespace Application.Auth;

public class RegisterUser
{
    public record Request(Credentials Credentials) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly IIdentityService _identityService;

        public Handler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _identityService.RegisterUser(request.Credentials.UserName, request.Credentials.Password);
        }
    }
}