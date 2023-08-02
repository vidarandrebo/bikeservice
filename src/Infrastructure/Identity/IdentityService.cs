using Application.Interfaces;
using Domain;
using Domain.Auth;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<DataResponse<UserData>> LoginUser(string userName, string password)
    {
        var err = new List<string>();
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            err.Add("User not found");
            return new DataResponse<UserData>(new UserData(Guid.Empty, ""), err.ToArray());
        }

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (result)
        {
            if (user.UserName != null)
                return new DataResponse<UserData>(new UserData(user.Id, user.UserName), Array.Empty<string>());
        }

        err.Add("Password is wrong");

        return new DataResponse<UserData>(new UserData(Guid.Empty, ""), err.ToArray());
    }

    public async Task<Unit> LogoutUser()
    {
        await _signInManager.SignOutAsync();
        return Unit.Value;
    }

    public async Task<Result<Guid>> RegisterUser(string userName, string password)
    {
        var user = new User
        {
            UserName = userName
        };
        var registerResult = await _userManager.CreateAsync(user, password);
        Console.WriteLine(user.Id);
        var errors = new List<Exception>();
        foreach (var err in registerResult.Errors)
        {
            errors.Add(new Exception(err.Description));
        }

        if (errors.Count > 0)
        {
            return new Result<Guid>(new AggregateException(errors));
        }

        return new Result<Guid>(user.Id);
    }
}