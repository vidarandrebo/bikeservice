namespace Domain.Auth;

public record AuthRouteResponse(string UserName, string token, string[] Errors);