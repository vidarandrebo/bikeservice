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
        var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userIdResult.IsSuccess)
        {
            var result = await _mediator.Send(new GetBikes.Request(userIdResult.Value));
            return Ok(new DataResponse<BikeDto[]>(result.Value, Array.Empty<string>()));
        }

        return Unauthorized(new DataResponse<BikeDto[]>(Array.Empty<BikeDto>(), new[] { "Not logged in" }));
    }

    [HttpPost]
    public async Task<IActionResult> AddBike(BikeFormDto bikeForm)
    {
        var ctSrc = new CancellationTokenSource();
        var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userIdResult.IsFailed)
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(new AddBike.Request(bikeForm, userIdResult.Value), ctSrc.Token);
        if (result.IsSuccess)
        {
            return Created(nameof(AddBike), bikeForm);
        }

        return BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> EditBike(BikeFormDto bikeForm)
    {
        var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userIdResult.IsFailed)
        {
            return Unauthorized();
        }

        var editBikeResult = await _mediator.Send(new EditBike.Request(bikeForm, userIdResult.Value));
        if (editBikeResult.IsSuccess)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBike(string id)
    {
        var bikeId = await GuidHelper.GuidOrEmptyAsync(id);
        var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userIdResult.IsFailed)
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(new DeleteBike.Request(bikeId, userIdResult.Value));
        if (result.IsFailed)
        {
            return BadRequest();
        }

        return Ok();
    }
}
