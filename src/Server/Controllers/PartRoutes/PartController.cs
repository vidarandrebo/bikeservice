using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application;
using BikeService.Domain.Parts.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeService.Server.Controllers.PartRoutes;

[ApiController]
[Route("api/[controller]")]
public class PartController : Controller
{
    private readonly IPartRepository _partRepository;

    public PartController(IPartRepository partRepository)
    {
        _partRepository = partRepository;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType<PartResponse[]>(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetParts(CancellationToken ct)
    {
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _partRepository.GetParts(userIdResult.Value, ct);
            if (result.IsSuccess)
            {
                return Ok(result.Value.Select(p => p.ToResponse()).ToArray());
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType<PartResponse>(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddPart(PostPartRequest postPartForm, CancellationToken ct)
    {
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _partRepository.AddPart(postPartForm, userIdResult.Value, ct);
            if (result.IsSuccess)
            {
                return Created(nameof(AddPart), result.Value.ToResponse());
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpDelete]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> DeletePart(string id, CancellationToken ct)
    {
        var partId = GuidHelper.GuidOrEmpty(id);
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var result = await _partRepository.DeletePart(partId, userIdResult.Value, ct);
            if (result.IsSuccess)
            {
                return Ok();
            }
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPut]
    [ProducesResponseType<PartResponse>(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> EditPart(PutPartRequest putPartRequest, CancellationToken ct)
    {
        var userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsSuccess)
        {
            var editPartResult = await _partRepository.EditPart(putPartRequest, userIdResult.Value, ct);
            if (editPartResult.IsSuccess)
            {
                return Ok(editPartResult.Value.ToResponse());
            }
        }

        return BadRequest();
    }
}