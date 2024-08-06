using System.Threading.Tasks;
using Application.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.AuthRoutes;

[ApiController]
[Route("api/[controller]")]
public class LogoutController : Controller
{
    private readonly IMediator _mediator;

    public LogoutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        await _mediator.Send(new LogoutUser.Request());
        return Ok();
    }
}