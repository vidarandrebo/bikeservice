using Application.Interfaces;
using MediatR;

namespace Application.Auth;

public class LogoutUser
{
    public record Request() : IRequest;

    public class Handler : IRequestHandler<Request, Unit>
    {
        private readonly IIdentityService _identityService;

        public Handler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _identityService.LogoutUser();
        }
    }
}