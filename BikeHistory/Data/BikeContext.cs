using BikeHistory.Domain.Auth;
using BikeHistory.Domain.Bike;
using Microsoft.EntityFrameworkCore;

namespace BikeHistory.Data;
public class BikeContext : DbContext {
    public DbSet<Bike> Bikes {get; set;} = null!;
    public DbSet<User> Users {get; set;} = null!;

    protected override void OnModelCreating(ModelBuilder modelbuilder) {
        base.OnModelCreating(modelbuilder);
    }
    public BikeContext(DbContextOptions options) : base(options) {}
}
