using System;
using System.Linq;
using BikeService.Domain.Bikes.Dtos;

namespace BikeService.Domain.Bikes.Contracts;

public class BikeResponse
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public double Mileage { get; set; }
    public DateTime Date { get; set; }
    public Guid TypeId { get; set; }

    public BikeResponse(Guid id, string manufacturer, string model, double mileage, DateTime date, Guid typeId)
    {
        Id = id;
        Manufacturer = manufacturer;
        Model = model;
        Mileage = mileage;
        Date = date;
        TypeId = typeId;
    }

    public static BikeResponse FromDto(BikeDto dto)
    {
        var bikeResponse = new BikeResponse(dto.Id, dto.Manufacturer, dto.Model, dto.Mileage, dto.Date, dto.TypeId);
        return bikeResponse;
    }

    public static BikeResponse[] FromDtos(BikeDto[] dtos)
    {
        var responses = dtos.Select(FromDto).ToArray();
        return responses;
    }
}