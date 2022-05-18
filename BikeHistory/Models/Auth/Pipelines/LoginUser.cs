using BikeHistory.Controllers.AuthRoutes;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BikeHistory.Models.Auth.Pipelines;

public class LoginUser
{
    public record Request(Credentials Credentials) : IRequest<SuccessResponse>;

    public class Handler : IRequestHandler<Request, SuccessResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public Handler(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<SuccessResponse> Handle(Request request, CancellationToken cancellationToken)
        {
            var err = new List<string>();
            var user = await _userManager.FindByNameAsync(request.Credentials.UserName);
            if (user is null)
            {
                err.Add("User not found");
                return new SuccessResponse(false, err.ToArray());
            }

            var loginResult =
                await _signInManager.PasswordSignInAsync(user, request.Credentials.Password, false, false);
            if (!loginResult.Succeeded)
            {
                err.Add("Password is wrong");
                return new SuccessResponse(false, err.ToArray());
            }

            return new SuccessResponse(true, Array.Empty<string>());
        }
    }
}