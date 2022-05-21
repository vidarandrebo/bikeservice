using System.Security.Claims;
using BikeHistory.Models;

namespace BikeHistory.Controllers.AuthRoutes;

/// <summary>Class for retrieving user data from session cookie</summary>
public static class UserDataFromCookie
{
    /// <summary>Retrieves username from session cookie</summary>
    /// <returns>Username in the form of a string, or null if not logged in</returns>
    public static string? GetUserName(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimTypes.Name);
    }

    /// <summary>Retrieves user id from session cookie</summary>
    /// <returns>User id in the form of a Guid, or Guid.Empty if not logged in</returns>
    public static Guid GetUserId(this HttpContext context)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return GuidHelper.GuidOrEmpty(userId);
    }
}