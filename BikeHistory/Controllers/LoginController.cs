using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BikeHistory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BikeHistory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        // GET
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            var currentUser = HttpContext.User;
            DateTime TokenDate = new DateTime();
            if (currentUser.HasClaim(c => c.Type == "Date"))
            {
                TokenDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Date").Value);
            }

            return Ok("Custom Claims(date): " + TokenDate);
        }

        //[AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            var user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWT(user);
                return Ok(new {token = tokenString});
            } 
            return Unauthorized();
        }
        
        

        private LoginModel AuthenticateUser(LoginModel loginData)
        {
            LoginModel user = null;
            if (loginData.UserName == "vidar")
            {
                user = new LoginModel {UserName = "vidar"};
            }

            return user;
        }

        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        private string GenerateJWT(LoginModel userData)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtAuth:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userData.UserName),
                new Claim("Date", DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["JwtAuth:Issuer"],
                _config["JwtAuth:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}