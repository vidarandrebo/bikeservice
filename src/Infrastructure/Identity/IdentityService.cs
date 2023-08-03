using Application.Interfaces;
using Domain.Auth;
using FluentResults;
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

    public async Task<Result<UserData>> LoginUser(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            return Result.Fail((new Error("User not found")));
        }
        var correctPasswd = await _userManager.CheckPasswordAsync(user, password);
        if (!correctPasswd)
        {
            return Result.Fail(new Error("Password is wrong"));
        }

        if (user.UserName is null)
        {
            return Result.Fail(new Error("Username is null"));
        }
        return Result.Ok(new UserData(user.Id, user.UserName));
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
        var errors = new List<Error>();
        foreach (var err in registerResult.Errors)
        {
            errors.Add(new Error(err.Description));
        }

        if (errors.Count > 0)
        {
            return Result.Fail(errors);
        }

        return Result.Ok(user.Id);
    }
}