using System.Threading;
using System.Threading.Tasks;
using BikeService.Application;
using BikeService.Application.Interfaces;
using BikeService.Domain.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeService.Server.Controllers.TypeRoutes;

[ApiController]
[Route("api/[controller]")]
public class TypeController : Controller
{
    private readonly ITypeRepository _typeRepository;
    private readonly ITokenHandler _tokenHandler;

    public TypeController(ITokenHandler tokenHandler, ITypeRepository typeRepository)
    {
        _tokenHandler = tokenHandler;
        _typeRepository = typeRepository;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<EquipmentTypeResponse[]>> GetTypes(CancellationToken ct)
    {
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _typeRepository.GetTypes(userIdResult.Value, ct);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddType(PostEquipmentTypeRequest typeForm, CancellationToken ct)
    {
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _typeRepository.AddType(typeForm.Name, typeForm.Category, userIdResult.Value, ct);

            if (result.IsSuccess)
            {
                return Created(nameof(AddType), typeForm);
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpDelete]
    public async Task<ActionResult> DeleteType(string id, CancellationToken ct)
    {
        var typeId = GuidHelper.GuidOrEmpty(id);
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _typeRepository.DeleteType(typeId, userIdResult.Value, ct);
            if (result.IsSuccess)
            {
                return Ok();
            }
        }


        return BadRequest();
    }
}