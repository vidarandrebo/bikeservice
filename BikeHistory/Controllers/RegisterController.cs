using BikeHistory.Data;
using BikeHistory.Domain.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BikeHistory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        BikeContext _db { get; set; }
        public RegisterController(BikeContext db)
        {
            _db = db;
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> OnPostAsync(LogRegModel registerData)
        {
            var user = new User(
                registerData.UserName,
                PasswordHasher.Hash(registerData.Password)
            );
            await Register.RegisterUser(_db, user);
            return Created(nameof(OnPostAsync), user.UserName);
        }

    }
}