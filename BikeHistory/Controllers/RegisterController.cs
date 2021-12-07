using Microsoft.AspNetCore.Mvc;

/*
namespace BikeHistory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        private IUserProvider _provider;
        public RegisterController(ITokenAuthenticator authenticator, IUserProvider provider) {
            _tokenAuthenticator = authenticator;
            _provider = provider;
        }
        private ITokenAuthenticator _tokenAuthenticator;

        // GET
        [HttpGet]
        public ActionResult<IEnumerable<string>> OnGet()
        {
            var currentUser = HttpContext.User;
            string authToken = Request.Headers["Authorization"].ToString();
            if (_tokenAuthenticator.ValidateToken(authToken)) {
                Console.WriteLine(_tokenAuthenticator.GetUserId(authToken));
                return Ok("Custom Claims(date): ");
            }
            return Unauthorized("You are not supposed to be here");
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> OnPostAsync(LoginModel login)
        {
            var user = new User{
                UserName = login.UserName,
                Password = login.Password
            };
            Console.WriteLine($"{login.UserName} {login.Password} {login.Date}");
            await _provider.AddUser(user);
            return Created("/Register", login.UserName);
        }

    }
}
*/