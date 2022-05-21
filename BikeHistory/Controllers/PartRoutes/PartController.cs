using BikeHistory.Controllers.AuthRoutes;
using BikeHistory.Models;
using BikeHistory.Models.Parts;
using BikeHistory.Models.Parts.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BikeHistory.Controllers.PartRoutes;

[ApiController]
[Route("[controller]")]
public class PartController : Controller
{
    private readonly IMediator _mediator;

    public PartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<DataResponse<PartDto[]>>> GetParts()
    {
        var userId = HttpContext.GetUserId();
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
        var result = await _mediator.Send(new AddPart.Request(partForm));
        if (result.Success)
        {
            return Created(nameof(AddPart), partForm);
        }

        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePart(string id)
    {
        var partId = await GuidHelper.GuidOrEmptyAsync(id);
        return Ok();
    }
}