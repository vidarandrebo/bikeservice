using Microsoft.AspNetCore.Mvc;

namespace BikeHistory.Controllers.Auth;


[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}