namespace BikeHistory.Controllers.AuthRoutes;

public record AuthRouteResponse(string UserName, string token, string[] Errors);