using System;

namespace BikeHistory.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

    }
}