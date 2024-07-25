using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.TestHelpers;

public static class DatabaseHelper
{
    public static ApplicationDbContext NewContext()
    {
        var conn = new SqliteConnection("Filename=:memory:");
        conn.Open();

        var contextOptions = new DbContextOptionsBuilder<SqliteContext>()
            .UseSqlite(conn, x => x.MigrationsAssembly("BikeService.Migrations.Sqlite"))
            .Options;

        var ctx = new SqliteContext(contextOptions);
        ctx.Database.Migrate();

        return ctx;
    }
}