using BikeHistory.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Domain.Auth.Pipelines;

public class LoginUser
{
    public record Request(Credentials Credentials) : IRequest<DataResponse<UserData>>;

    public class Handler : IRequestHandler<Request, DataResponse<UserData>>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public Handler(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<DataResponse<UserData>> Handle(Request request, CancellationToken cancellationToken)
        {
            var err = new List<string>();
            var user = await _userManager.FindByNameAsync(request.Credentials.UserName);
            if (user is null)
            {
                err.Add("User not found");
                return new DataResponse<UserData>(new UserData(Guid.Empty, ""), err.ToArray());
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Credentials.Password);
            if (result)
            {
                return new DataResponse<UserData>(new UserData(user.Id, user.UserName), Array.Empty<string>());
            }
            err.Add("Password is wrong");

            return new DataResponse<UserData>(new UserData(Guid.Empty,  ""), err.ToArray());
        }
    }
}