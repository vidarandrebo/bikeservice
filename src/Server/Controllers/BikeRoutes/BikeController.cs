using System;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application;
using BikeService.Application.Interfaces;
using BikeService.Domain.Bikes.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BikeService.Server.Controllers.BikeRoutes;

[ApiController]
[Route("api/[controller]")]
public class BikeController : Controller
{
    private readonly ITokenHandler _tokenHandler;
    private readonly ILogger<BikeController> _logger;
    private readonly IBikeRepository _bikeRepository;

    public BikeController(ITokenHandler tokenHandler, ILogger<BikeController> logger, IBikeRepository bikeRepository)
    {
        _tokenHandler = tokenHandler;
        _logger = logger;
        _bikeRepository = bikeRepository;
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
    public async Task<ActionResult<BikeResponse[]>> GetBikes(CancellationToken ct)
    {
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            //var result = await _mediator.Send(new GetBikes.Request(userIdResult.Value));
            var result = await _bikeRepository.GetBikes(userIdResult.Value, ct);
            if (result.IsSuccess)
            {
                return Ok(BikeResponse.FromDtos(result.Value));
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddBike(PostBikeRequest postBikeRequest, CancellationToken ct)
    {
        var userIdResult = HttpContext.GetUserId();

        if (userIdResult.IsSuccess)
        {
            var result = await _bikeRepository.AddBike(postBikeRequest, userIdResult.Value, ct);
            if (result.IsSuccess)
            {
                return Created(nameof(AddBike), postBikeRequest);
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> EditBike(PutBikeRequest putBikeRequest, CancellationToken ct)
    {
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var editBikeResult = await _bikeRepository.EditBike(putBikeRequest, userIdResult.Value, ct);
            if (editBikeResult.IsSuccess)
            {
                return Ok();
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteBike(string id, CancellationToken ct)
    {
        var bikeId = GuidHelper.GuidOrEmpty(id);
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _bikeRepository.DeleteBike(bikeId, userIdResult.Value, ct);
            if (result.IsSuccess)
            {
                return Ok();
            }
        }

        return BadRequest();
    }
}