using BikeHistory.Models.Auth;
using BikeHistory.Models.Auth.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BikeHistory.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(HttpContext.GetUserName());
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> LoginUser(Credentials credentials)
    {
        var result = await _mediator.Send(new LoginUser.Request(credentials));
        if (result.Success)
        {
            return Ok(result);
        }

        return Unauthorized(result);
    }
}