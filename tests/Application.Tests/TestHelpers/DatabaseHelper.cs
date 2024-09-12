using BikeService.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BikeService.Application.Tests.TestHelpers;

public static class DatabaseHelper
{
    public static ApplicationDbContext NewContext()
    {
        var conn = new SqliteConnection("Filename=:memory:");
        conn.Open();

        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(conn, x => x.MigrationsAssembly("BikeService.Migrations.Sqlite"))
            .Options;

        var ctx = new ApplicationDbContext(contextOptions);
        ctx.Database.Migrate();

        return ctx;
    }
}