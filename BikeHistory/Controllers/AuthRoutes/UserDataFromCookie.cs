using System.Security.Claims;
using BikeHistory.Models;

namespace BikeHistory.Controllers.AuthRoutes;

public static class UserDataFromCookie
{
    public static string? GetUserName(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimTypes.Name);
    }

    public static Guid GetUserId(this HttpContext context)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return GuidHelper.GuidOrEmpty(userId);
    }
}