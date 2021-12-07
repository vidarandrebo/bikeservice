using BikeHistory.Domain.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BikeHistory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

/*
        [HttpGet]
        public User Get()
        {
            var user = new User();
            user.UserName = "hei";
            user.Password = "test";
            user.Id = 4;
            return user;
        }
        */

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            Console.WriteLine($"{user.Id}, {user.Password}, {user.UserName}");
            return CreatedAtAction("User", user);
        }
    }
}
