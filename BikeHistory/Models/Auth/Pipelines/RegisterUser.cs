using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BikeHistory.Models.Auth.Pipelines;

public class RegisterUser
{
    public record Request(Credentials Credentials) : IRequest<AuthResponse>;

    public class Handler : IRequestHandler<Request, AuthResponse>
    {
        private readonly UserManager<User> _userManager;

        public Handler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.Credentials.UserName
            };
            var registerResult = await _userManager.CreateAsync(user, request.Credentials.Password);
            var errors = new List<string>();
            foreach (var err in registerResult.Errors)
            {
                errors.Add(err.Description);
            }

            return new AuthResponse(registerResult.Succeeded, errors.ToArray());
        }
    }
}