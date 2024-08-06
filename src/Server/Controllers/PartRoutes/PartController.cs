using System;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Application.Parts.Commands;
using BikeService.Domain;
using BikeService.Domain.Parts.Contracts;
using BikeService.Domain.Parts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BikeService.Server.Controllers.PartRoutes;

[ApiController]
[Route("api/[controller]")]
public class PartController : Controller
{
    private readonly IMediator _mediator;
    private readonly ITokenHandler _tokenHandler;

    public PartController(IMediator mediator, ITokenHandler tokenHandler)
    {
        _tokenHandler = tokenHandler;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<DataResponse<PartDto[]>>> GetParts()
    {
        //var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _mediator.Send(new GetParts.Request(userIdResult.Value));
            return Ok(new DataResponse<PartDto[]>(result.Value, Array.Empty<string>()));
        }

        return Unauthorized(new DataResponse<PartDto[]>(Array.Empty<PartDto>(), new[] { "Not logged in" }));
    }

    [HttpPost]
    public async Task<IActionResult> AddPart(PartFormDto partForm)
    {
        //var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _mediator.Send(new AddPart.Request(partForm, userIdResult.Value));
            if (result.IsSuccess)
            {
                return Created(nameof(AddPart), partForm);
            }

            return BadRequest();
        }

        return Unauthorized();
    }

    [HttpDelete]
    public async Task<ActionResult> DeletePart(string id)
    {
        var partId = GuidHelper.GuidOrEmpty(id);
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsFailed)
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(new DeletePart.Request(partId, userIdResult.Value));
        if (result.IsSuccess)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> EditPart(PartFormDto partForm)
    {
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsFailed)
        {
            return Unauthorized();
        }

        var editPartResult = await _mediator.Send(new EditPart.Request(partForm, userIdResult.Value));
        if (editPartResult.IsSuccess)
        {
            return Ok();
        }

        return BadRequest();
    }
}