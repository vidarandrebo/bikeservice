namespace BikeHistory.Models.Bikes;

public record BikeResponse(BikeDto[] Bikes, string[] Errors);