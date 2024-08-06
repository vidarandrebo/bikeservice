using System;
using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using BikeService.Application.Types;
using BikeService.Domain;
using BikeService.Domain.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BikeService.Server.Controllers.TypeRoutes;

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
        var userIdResult = HttpContext.GetUserId();
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
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result =
                await _mediator.Send(new AddType.Request(typeForm.Name, typeForm.Category, userIdResult.Value));
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
        var userIdResult = HttpContext.GetUserId();
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