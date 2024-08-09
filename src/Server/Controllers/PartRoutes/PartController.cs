using System.Threading.Tasks;
using BikeService.Application.Parts.Commands;
using BikeService.Domain;
using BikeService.Domain.Parts.Contracts;
using BikeService.Domain.Parts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeService.Server.Controllers.PartRoutes;

[ApiController]
[Route("api/[controller]")]
public class PartController : Controller
{
    private readonly IMediator _mediator;

    public PartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PartResponse[]>> GetParts()
    {
        //var userIdResult = _tokenHandler.GetUserIdFromRequest(HttpContext);
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _mediator.Send(new GetParts.Request(userIdResult.Value));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddPart(PostPartRequest postPartForm)
    {
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _mediator.Send(new AddPart.Request(postPartForm, userIdResult.Value));
            if (result.IsSuccess)
            {
                return Created(nameof(AddPart), postPartForm);
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpDelete]
    public async Task<ActionResult> DeletePart(string id)
    {
        var partId = GuidHelper.GuidOrEmpty(id);
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _mediator.Send(new DeletePart.Request(partId, userIdResult.Value));
            if (result.IsSuccess)
            {
                return Ok();
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> EditPart(PutPartRequest putPartRequest)
    {
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var editPartResult = await _mediator.Send(new EditPart.Request(putPartRequest, userIdResult.Value));
            if (editPartResult.IsSuccess)
            {
                return Ok();
            }
        }

        return BadRequest();
    }
}