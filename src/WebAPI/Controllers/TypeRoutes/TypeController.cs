using Application.Types;
using Domain;
using Domain.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers.TypeRoutes;

[ApiController]
[Route("api/[controller]")]
public class TypeController : Controller
{
    private readonly IMediator _mediator;
    private readonly ITokenHandler _tokenHandler;

    public TypeController(IMediator mediator, ITokenHandler tokenHandler)
    {
        _tokenHandler = tokenHandler;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<DataResponse<EquipmentTypeDto[]>>> GetTypes()
    {
        var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userIdResult.IsSuccess)
        {
            var result = await _mediator.Send(new GetTypes.Request(userIdResult.Value));
            return Ok(new DataResponse<EquipmentTypeDto[]>(result.Value, Array.Empty<string>()));
        }

        return Unauthorized(
            new DataResponse<EquipmentTypeDto[]>(Array.Empty<EquipmentTypeDto>(), new[] { "Not logged in" }));
    }

    [HttpPost]
    public async Task<IActionResult> AddType(EquipmentTypeFormDto typeForm)
    {
        var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userIdResult.IsSuccess)
        {
            var result = await _mediator.Send(new AddType.Request(typeForm.Name, typeForm.Category, userIdResult.Value));
            if (result.IsSuccess)
            {
                return Created(nameof(AddType), typeForm);
            }

            return BadRequest();
        }

        return Unauthorized();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteType(string id)
    {
        var typeId = GuidHelper.GuidOrEmpty(id);
        var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userIdResult.IsFailed)
        {
            return Unauthorized();
        }
        var result = await _mediator.Send(new DeleteType.Request(typeId, userIdResult.Value));
        if (result.IsSuccess)
        {
            return Ok();
        }

        return BadRequest();
    }
}