using Application.Auth;
using BikeHistory.Controllers.AuthRoutes;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.AuthRoutes;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : Controller
{
    private readonly IMediator _mediator;

    public RegisterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> RegisterUser(Credentials credentials)
    {
        var result = await _mediator.Send(new RegisterUser.Request(credentials));
        if (result.Success)
        {
            return Created(nameof(RegisterUser), new AuthRouteResponse(credentials.UserName, "", result.Errors));
        }

        return Unauthorized(new AuthRouteResponse(credentials.UserName, "", result.Errors));
    }
}