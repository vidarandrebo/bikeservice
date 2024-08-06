namespace BikeService.Infrastructure.Identity;

public record AuthRouteResponse(string UserName, string Token, string[] Errors);