using BikeHistory.Models.Bikes;
using BikeHistory.Models.Bikes.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BikeHistory.Controllers.BikeRoutes;

[ApiController]
[Route("[controller]")]
public class BikeController : Controller
{
    private readonly IMediator _mediator;

    public BikeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<BikeResponse> GetBikes()
    {
        var bikes = await _mediator.Send(new GetBikes.Request());
        return bikes;
    }

    [HttpPost]
    public async Task<IActionResult> AddBike(BikeDTO bike)
    {
        var result = await _mediator.Send(new AddBike.Request(bike));
        if (result.Success)
        {
            return Created(nameof(AddBike), bike);
        }

        return BadRequest();
    }

}