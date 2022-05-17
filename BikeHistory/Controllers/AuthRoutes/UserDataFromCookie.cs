using System.Security.Claims;

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
        if (userId is not null)
        {
            return new Guid(userId);
        }

        return Guid.Empty;
    }
}