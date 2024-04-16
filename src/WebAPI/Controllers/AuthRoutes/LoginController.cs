using Application.Auth;
using Domain.Auth;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers.AuthRoutes;

[ApiController]
[Route("api/[controller]")]
public class LoginController : Controller
{
    private readonly IMediator _mediator;
    private readonly ITokenHandler _tokenHandler;

    public LoginController(IMediator mediator, ITokenHandler tokenHandler)
    {
        _mediator = mediator;
        _tokenHandler = tokenHandler;
    }

    // GET
    [HttpGet]
    public IActionResult Index()
    {
        var userName = _tokenHandler.GetUserNameFromRequest(HttpContext);
        if (userName is not null)
        {
            return Ok(new AuthRouteResponse(userName, "", Array.Empty<string>()));
        }

        return Ok(new AuthRouteResponse("", "", new[] { "Not logged in" }));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> LoginUser(Credentials credentials)
    {
        var result = await _mediator.Send(new LoginUser.Request(credentials));

        if (result.IsSuccess)
        {
            var data = result.Value;
            var token = _tokenHandler.CreateToken(data.Id, data.UserName);
            return Ok(new AuthRouteResponse(credentials.UserName, token, Array.Empty<string>()));
        }

        return Unauthorized(new AuthRouteResponse(credentials.UserName, "",
            result.Errors.Select(e => e.Message).ToArray()));
    }
}