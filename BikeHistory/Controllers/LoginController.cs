using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BikeHistory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BikeHistory.Services;

namespace BikeHistory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        public LoginController(ITokenAuthenticator authenticator) {
            _tokenAuthenticator = authenticator;
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
            return Unauthorized();
        }

        //POST
        [HttpPost]
        public IActionResult OnPost(LoginModel login)
        {
            var tokenString = _tokenAuthenticator.GenerateToken(4);
            return Ok(new { token = tokenString});
        }

    }
}
