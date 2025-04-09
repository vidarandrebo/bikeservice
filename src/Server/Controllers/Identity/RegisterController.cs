using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application;
using BikeService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BikeService.Server.Controllers.Identity;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly ITypeRepository _typeRepository;
    private readonly ILogger<LoginController> _logger;
    private readonly IConfiguration _cfg;

    public RegisterController(ILogger<LoginController> logger, IIdentityService identityService, IConfiguration cfg, ITypeRepository typeRepository)
    {
        _logger = logger;
        _identityService = identityService;
        _cfg = cfg;
        _typeRepository = typeRepository;
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<HttpValidationProblemDetails>> Post(RegisterRequest registerRequest, CancellationToken ct)
    {
        var ctSource = new CancellationTokenSource(_cfg.GetValue<int>("CancellationToken:Delay"));
        var registerUserResult = await _identityService.RegisterUser(registerRequest.Email, registerRequest.Password);
        if (registerUserResult.IsFailed)
        {
            var errs = registerUserResult.Errors.Select(x => x.ToString() ?? "").ToArray();
            return BadRequest(new HttpValidationProblemDetails(
                new Dictionary<string, string[]>()
                {
                    { "register", errs }
                }
            ));
        }

        await _typeRepository.AddDefaultTypes(registerUserResult.Value, ct);

        return Created();
    }
}