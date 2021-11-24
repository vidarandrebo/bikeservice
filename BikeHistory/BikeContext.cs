using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BikeHistory.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeHistory {
    public class BikeContext : DbContext {
        public DbSet<Bike> Bikes {get; set;}
        public DbSet<User> Users {get; set;}

        protected override void OnModelCreating(ModelBuilder modelbuilder) {
            base.OnModelCreating(modelbuilder);
        }
        public BikeContext(DbContextOptions options) : base(options) {}
    }
}
