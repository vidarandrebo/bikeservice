using System.Security.Claims;

namespace BikeHistory.Models.Auth;

public static class UserDataFromCookie
{
    public static string? GetUserName(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimTypes.Name);
    }
    
}