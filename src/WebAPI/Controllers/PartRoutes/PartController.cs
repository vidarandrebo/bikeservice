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
        var userId = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userId != Guid.Empty)
        {
            var parts = await _mediator.Send(new GetParts.Request(userId));
            return Ok(parts);
        }

        return Unauthorized(new DataResponse<PartDto[]>(Array.Empty<PartDto>(), new[] {"Not logged in"}));
    }

    [HttpPost]
    public async Task<IActionResult> AddPart(PartFormDto partForm)
    {
        var userId = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userId != Guid.Empty)
        {
            var result = await _mediator.Send(new AddPart.Request(partForm,userId));
            if (result.Success)
            {
                return Created(nameof(AddPart), partForm);
            }

            return BadRequest();
        }

        return Unauthorized();
    }

    [HttpDelete]
    public async Task<ActionResult<SuccessResponse>> DeletePart(string id)
    {
        var partId = GuidHelper.GuidOrEmpty(id);
        var userId = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userId == Guid.Empty)
        {
            return Unauthorized(new SuccessResponse(false, new[] {"Not logged in"}));
        }

        var result = await _mediator.Send(new DeletePart.Request(partId, userId));
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}