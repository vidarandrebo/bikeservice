using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class NpgsqlContext : ApplicationDbContext
{
    public NpgsqlContext(DbContextOptions<NpgsqlContext> conf) : base(conf)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}