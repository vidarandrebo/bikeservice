using System;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application.Bikes;
using BikeService.Application.Interfaces;
using BikeService.Domain;
using BikeService.Domain.Bikes.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BikeService.Server.Controllers.BikeRoutes;

[ApiController]
[Route("api/[controller]")]
public class BikeController : Controller
{
    private readonly IMediator _mediator;
    private readonly ITokenHandler _tokenHandler;
    private readonly ILogger<BikeController> _logger;

    public BikeController(IMediator mediator, ITokenHandler tokenHandler, ILogger<BikeController> logger)
    {
        _mediator = mediator;
        _tokenHandler = tokenHandler;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public ActionResult<BikeResponse> GetBike(Guid id)
    {
        // TODO - allow fetching of a single bike
//        var userIdResult = HttpContext.GetUserId();
//        if (userIdResult.IsSuccess)
//        {
//            var result = await _mediator.Send(new GetBikes.Request(userIdResult.Value));
//            return Ok(BikeResponse.FromDtos(result.Value));
//        }

        return NoContent();
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<BikeResponse[]>> GetBikes()
    {
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _mediator.Send(new GetBikes.Request(userIdResult.Value));
            return Ok(BikeResponse.FromDtos(result.Value));
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddBike(PostBikeRequest postBikeRequest)
    {
        var ctSrc = new CancellationTokenSource();
        var userIdResult = HttpContext.GetUserId();

        if (userIdResult.IsSuccess)
        {
            var result = await _mediator.Send(new AddBike.Request(postBikeRequest, userIdResult.Value), ctSrc.Token);
            if (result.IsSuccess)
            {
                return Created(nameof(AddBike), postBikeRequest);
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> EditBike(PutBikeRequest postBikeForm)
    {
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var editBikeResult = await _mediator.Send(new EditBike.Request(postBikeForm, userIdResult.Value));
            if (editBikeResult.IsSuccess)
            {
                return Ok();
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteBike(string id)
    {
        var bikeId = GuidHelper.GuidOrEmpty(id);
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _mediator.Send(new DeleteBike.Request(bikeId, userIdResult.Value));
            if (result.IsSuccess)
            {
                return Ok();
            }
        }

        return BadRequest();
    }
}