using Application.Parts;
using Application.Tests.TestHelpers;
using Domain.Parts;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.Parts;

public class AddPartTest
{
    private readonly ApplicationDbContext _db;

    public AddPartTest()
    {
        _db = DatabaseHelper.NewContext();
    }

    [Fact]
    public async Task AddOnePart()
    {
        var bikeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var typeId = Guid.NewGuid();
        var model = "broken";
        var manufacturer = "part";
        var mileage = 1234.56;
        var handler = new AddPart.Handler(_db);
        var partFormDto = new PartFormDto("", mileage, manufacturer, model, bikeId.ToString(),
            typeId.ToString());
        var request = new AddPart.Request(partFormDto, userId);
        await handler.Handle(request, new CancellationToken());

        var part = await _db.Parts.FirstOrDefaultAsync();
        Assert.NotNull(part);
        Assert.Equal(bikeId, part.BikeId);
        Assert.Equal(userId, part.UserId);
        Assert.Equal(typeId, part.TypeId);
        Assert.Equal(model, part.Model);
        Assert.Equal(manufacturer, part.Manufacturer);
        Assert.Equal(mileage, part.Mileage);
    }
}