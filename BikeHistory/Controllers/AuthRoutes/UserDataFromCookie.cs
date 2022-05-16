using System.Security.Claims;

namespace BikeHistory.Controllers.AuthRoutes;

public static class UserDataFromCookie
{
    public static string? GetUserName(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimTypes.Name);
    }
    
}