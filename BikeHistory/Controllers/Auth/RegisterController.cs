using BikeHistory.Models.Auth;
using BikeHistory.Models.Auth.Pipelines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BikeHistory.Controllers.Auth;

[ApiController]
[Route("[controller]")]
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
            return Created(nameof(RegisterUser), result);
        }

        return Unauthorized(result);
    }
}