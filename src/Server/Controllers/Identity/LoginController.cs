using System.Threading.Tasks;
using BikeService.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BikeService.Server.Controllers.Identity;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<LoginController> _logger;
    private readonly IIdentityService _identityService;

    public LoginController(IMediator mediator, ILogger<LoginController> logger, IIdentityService identityService)
    {
        _mediator = mediator;
        _logger = logger;
        _identityService = identityService;
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<AccessTokenResponse>> Post(LoginRequest loginRequest)
    {
        var response = await _identityService.LoginUser(loginRequest.Email, loginRequest.Password);
        if (response.IsFailed)
        {
            return BadRequest();
        }

        return Created(nameof(Post), response);
    }
}