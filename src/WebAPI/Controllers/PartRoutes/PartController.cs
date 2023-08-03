using Application.Parts;
using Domain;
using Domain.Parts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers.PartRoutes;

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
        var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
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
        var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
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
        var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userIdResult.IsSuccess)
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
}