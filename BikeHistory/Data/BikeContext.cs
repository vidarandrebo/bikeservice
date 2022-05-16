using BikeHistory.Models;
using BikeHistory.Models.Auth;
using BikeHistory.Models.Bikes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeHistory.Data;

public class BikeContext : IdentityDbContext<User>
{
    public DbSet<Bike> Bikes { get; set; } = null!;
    public DbSet<Part> Parts { get; set; } = null!;

    public BikeContext(DbContextOptions conf) : base(conf)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}