using Application.Bikes;
using Domain;
using Domain.Bikes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers.BikeRoutes;

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
            var result = await _mediator.Send(new GetBikes.Request(userId));
            return Ok(new DataResponse<BikeDto[]>(result.Value, Array.Empty<string>()));
        }

        return Unauthorized(new DataResponse<BikeDto[]>(Array.Empty<BikeDto>(), new[] {"Not logged in"}));
    }

    [HttpPost]
    public async Task<IActionResult> AddBike(BikeFormDto bikeForm)
    {
        var userId = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userId != Guid.Empty)
        {
            var result = await _mediator.Send(new AddBike.Request(bikeForm, userId));
            if (result.IsSuccess)
            {
                return Created(nameof(AddBike), bikeForm);
            }
            return BadRequest();
        }

        return Unauthorized();

    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBike(string id)
    {
        var bikeId = await GuidHelper.GuidOrEmptyAsync(id);
        var result =
            await _mediator.Send(new DeleteBike.Request(bikeId, _tokenHandler.GetUserIdFromRequest(HttpContext)));
        if (result.IsSuccess)
        {
            return Ok();
        }

        return NotFound();
    }
}