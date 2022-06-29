using BikeHistory.Controllers.AuthRoutes;
using BikeHistory.Models.Bikes;
using BikeHistory.Models.Bikes.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BikeHistory.Models;
using BikeHistory.Services;

namespace BikeHistory.Controllers.BikeRoutes;

[ApiController]
[Route("api/[controller]")]
public class BikeController : Controller
{
    private readonly IMediator _mediator;
    private readonly ITokenHandler _tokenHandler;

    public BikeController(IMediator mediator, ITokenHandler tokenHandler)
    {
        _mediator = mediator;
        _tokenHandler = tokenHandler;
    }

    [HttpGet]
    public async Task<ActionResult<DataResponse<BikeDto[]>>> GetBikes()
    {
        var userId = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userId != Guid.Empty)
        {
            var bikes = await _mediator.Send(new GetBikes.Request(userId));
            return Ok(bikes);
        }

        return Unauthorized(new DataResponse<BikeDto[]>(Array.Empty<BikeDto>(), new[] {"Not logged in"}));
    }

    [HttpPost]
    public async Task<IActionResult> AddBike(BikeFormDto bikeForm)
    {
        var result = await _mediator.Send(new AddBike.Request(bikeForm));
        if (result.Success)
        {
            return Created(nameof(AddBike), bikeForm);
        }

        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBike(string id)
    {
        var bikeId = await GuidHelper.GuidOrEmptyAsync(id);
        var result = await _mediator.Send(new DeleteBike.Request(bikeId, _tokenHandler.GetUserIdFromRequest(HttpContext)));
        if (result.Success)
        {
            return Ok();
        }

        return NotFound();
    }
}