using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using FluentResults;
using MediatR;

namespace Application.Auth;

public class LogoutUser
{
    public record Request() : IRequest<Result>;

    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IIdentityService _identityService;

        public Handler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            await _identityService.LogoutUser();
            return Result.Ok();
        }
    }
}