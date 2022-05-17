using BikeHistory.Controllers.AuthRoutes;
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
    public async Task<ActionResult<BikeResponse>> GetBikes()
    {
        var userId = HttpContext.GetUserId();
        if (userId != Guid.Empty)
        {
            var bikes = await _mediator.Send(new GetBikes.Request(userId));
            return Ok(bikes);
        }

        return Unauthorized(new BikeResponse(Array.Empty<Bike>(), new[] {"Not logged in"}));
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