using BikeHistory.Controllers.AuthRoutes;
using BikeHistory.Models;
using BikeHistory.Models.Types;
using BikeHistory.Models.Types.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BikeHistory.Controllers.TypeRoutes;

[ApiController]
[Route("api/[controller]")]
public class TypeController : Controller
{
    private readonly IMediator _mediator;

    public TypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<DataResponse<EquipmentTypeDto[]>>> GetTypes()
    {
        var userid = HttpContext.GetUserId();
        if (userid != Guid.Empty)
        {
            var types = await _mediator.Send(new GetTypes.Request(userid));
            return Ok(types);
        }

        return Unauthorized(
            new DataResponse<EquipmentTypeDto[]>(Array.Empty<EquipmentTypeDto>(), new[] { "Not logged in" }));
    }

    [HttpPost]
    public async Task<IActionResult> AddType(EquipmentTypeFormDto typeForm)
    {
        var userId = HttpContext.GetUserId();
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
        var userid = HttpContext.GetUserId();
        if (userid == Guid.Empty)
        {
            return Unauthorized(new SuccessResponse(false, new[] { "Not logged in" }));
        }
        var result = await _mediator.Send(new DeleteType.Request(typeId, userid));
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}