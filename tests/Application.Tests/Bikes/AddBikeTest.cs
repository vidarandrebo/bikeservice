using System;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Bikes;
using BikeService.Application.Tests.TestHelpers;
using BikeService.Domain.Bikes.Contracts;
using BikeService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BikeService.Application.Tests.Bikes;

public class AddBikeTest
{
    private readonly ApplicationDbContext _db;

    public AddBikeTest()
    {
        _db = DatabaseHelper.NewContext();
    }

    [Fact]
    public async Task AddOneBike()
    {
        var userId = Guid.NewGuid();
        var typeId = Guid.NewGuid();
        var model = "fast";
        var manufacturer = "bike";
        var mileage = 1234.56;
        var handler = new AddBike.Handler(_db);
        var bikeFormDto = new BikeFormDto("", mileage, model, manufacturer, DateTime.Now,
            typeId.ToString());
        var request = new AddBike.Request(bikeFormDto, userId);
        await handler.Handle(request, new CancellationToken());

        var bike = await _db.Bikes.FirstOrDefaultAsync();
        Assert.NotNull(bike);
        Assert.Equal(userId, bike.UserId);
        Assert.Equal(typeId, bike.TypeId);
        Assert.Equal(model, bike.Model);
        Assert.Equal(manufacturer, bike.Manufacturer);
        Assert.Equal(mileage, bike.Mileage);
    }
}