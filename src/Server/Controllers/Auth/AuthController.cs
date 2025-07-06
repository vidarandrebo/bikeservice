using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BikeService.Application;
using BikeService.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BikeService.Server.Controllers.Auth;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IIdentityService _identityService;
    private readonly ITokenHandler _tokenHandler;
    private readonly ITypeRepository _typeRepository;
    private readonly IConfiguration _cfg;


    public AuthController(ILogger<AuthController> logger, IIdentityService identityService, ITokenHandler tokenHandler, ITypeRepository typeRepository, IConfiguration cfg)
    {
        _logger = logger;
        _identityService = identityService;
        _tokenHandler = tokenHandler;
        _typeRepository = typeRepository;
        _cfg = cfg;
    }

    // POST
    [HttpPost]
    [Route("api/[controller]/[action]")]
    public async Task<ActionResult<AccessTokenResponse>> Login(LoginRequest loginRequest)
    {
        var response = await _identityService.LoginUser(loginRequest.Email, loginRequest.Password);
        if (response.IsFailed)
        {
            return BadRequest();
        }

        return Created(nameof(Login), response.Value);
    }

    // POST
    [HttpPost]
    [Route("api/[controller]/[action]")]
    public ActionResult<AccessTokenResponse> Refresh(RefreshRequest refreshRequest)
    {
        _logger.LogInformation("refreshing token");

        var claimsResult = _tokenHandler.ValidateToken(refreshRequest.RefreshToken);

        if (claimsResult.IsFailed)
        {
            return Unauthorized();
        }

        var email = claimsResult.Value.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        var id = claimsResult.Value.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (email is null || id is null)
        {
            return BadRequest();
        }

        var idGuid = GuidHelper.GuidOrEmpty(id.Value);

        var response = new AccessTokenResponse()
        {
            AccessToken = _tokenHandler.AccessToken(idGuid, email.Value),
            RefreshToken = _tokenHandler.RefreshToken(idGuid, email.Value),
            ExpiresIn = 60 * 60 * 24,
        };


        return Ok(response);
    }
    // POST
    [HttpPost]
    [Route("api/[controller]/[action]")]
    [ProducesResponseType(201)]
    [ProducesResponseType<HttpValidationProblemDetails>(400)]
    public async Task<IActionResult> Register(RegisterRequest registerRequest, CancellationToken ct)
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
