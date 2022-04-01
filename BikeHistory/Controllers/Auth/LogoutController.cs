using BikeHistory.Models.Auth.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BikeHistory.Controllers.Auth;

[ApiController]
[Route("[controller]")]
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