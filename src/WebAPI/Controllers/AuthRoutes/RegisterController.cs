using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Auth;
using Application.Types;
using Domain.Auth;
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

        if (result.IsSuccess)
        {
            await _mediator.Send(new AddDefaultTypes.Request(result.Value));

            return Created(nameof(RegisterUser),
                new AuthRouteResponse(credentials.UserName, "", Array.Empty<string>()));
        }

        return Unauthorized(new AuthRouteResponse(credentials.UserName, "",
            result.Errors.Select(e => e.Message).ToArray()));
    }
}