namespace BikeService.Domain;

public record DataResponse<T>(T Data, string[] Errors);