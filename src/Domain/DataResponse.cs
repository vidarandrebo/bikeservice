namespace BikeHistory.Models;

public record DataResponse<T>(T Data, string[] Errors);