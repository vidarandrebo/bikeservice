using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class SqliteContext : ApplicationDbContext
{
    public SqliteContext(DbContextOptions<SqliteContext> conf) : base(conf)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}