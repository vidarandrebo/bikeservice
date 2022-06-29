using BikeHistory.Controllers.AuthRoutes;
using BikeHistory.Models;
using BikeHistory.Models.Types;
using BikeHistory.Models.Types.Pipelines;
using BikeHistory.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BikeHistory.Controllers.TypeRoutes;

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
        var userId = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userId != Guid.Empty)
        {
            var types = await _mediator.Send(new GetTypes.Request(userId));
            return Ok(types);
        }

        return Unauthorized(
            new DataResponse<EquipmentTypeDto[]>(Array.Empty<EquipmentTypeDto>(), new[] { "Not logged in" }));
    }

    [HttpPost]
    public async Task<IActionResult> AddType(EquipmentTypeFormDto typeForm)
    {
        var userId = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userId != Guid.Empty)
        {
            var result = await _mediator.Send(new AddType.Request(typeForm.Name, typeForm.Category, userId));
            if (result.Success)
            {
                return Created(nameof(AddType), typeForm);
            }

            return BadRequest();
        }

        return Unauthorized();
    }

    [HttpDelete]
    public async Task<ActionResult<SuccessResponse>> DeleteType(string id)
    {
        var typeId = GuidHelper.GuidOrEmpty(id);
        var userId = _tokenHandler.GetUserIdFromRequest(HttpContext);
        if (userId == Guid.Empty)
        {
            return Unauthorized(new SuccessResponse(false, new[] { "Not logged in" }));
        }
        var result = await _mediator.Send(new DeleteType.Request(typeId, userId));
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}