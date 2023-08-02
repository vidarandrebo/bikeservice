using Application.Auth;
using Application.Types;
using Infrastructure.Identity;
using LanguageExt.Pretty;
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
        var errors = Array.Empty<string>();

        var userId = result.Match(
            guid => guid,
            ex =>
            {
                errors = new[] { ex.Message };
                return Guid.Empty;
            }
        );
        if (result.IsSuccess)
        {
            await _mediator.Send(new AddDefaultTypes.Request(userId));
            
            return Created(nameof(RegisterUser),
                new AuthRouteResponse(credentials.UserName, "", Array.Empty<string>()));
        }
        return Unauthorized(new AuthRouteResponse(credentials.UserName, "", errors));
    }
}