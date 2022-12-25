using BikeHistory.Controllers.AuthRoutes;
using BikeHistory.Models.Auth.Pipelines;
using BikeHistory.Services;
using Domain.Auth;
using Domain.Auth.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(new AuthRouteResponse("", "", new[] {"Not logged in"}));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> LoginUser(Credentials credentials)
    {
        var result = await _mediator.Send(new LoginUser.Request(credentials));
        if (result.Errors.Length == 0)
        {
            var token = _tokenHandler.CreateToken(result.Data.Id, result.Data.UserName);
            return Ok(new AuthRouteResponse(credentials.UserName, token, result.Errors));
        }

        return Unauthorized(new AuthRouteResponse(credentials.UserName, "", result.Errors));
    }
}