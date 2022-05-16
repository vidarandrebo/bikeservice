using BikeHistory.Models.Auth.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BikeHistory.Controllers.AuthRoutes;

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
        var userName = HttpContext.GetUserName();
        if (userName is not null)
        {
            return Ok(new AuthRouteResponse(userName, Array.Empty<string>()));
        }

        return Unauthorized(new AuthRouteResponse("", new[] {"Not logged in"}));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> LoginUser(Credentials credentials)
    {
        var result = await _mediator.Send(new LoginUser.Request(credentials));
        if (result.Success)
        {
            return Ok(new AuthRouteResponse(credentials.UserName, result.Errors));
        }

        return Unauthorized(new AuthRouteResponse(credentials.UserName, result.Errors));
    }
}