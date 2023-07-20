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

        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(conn)
            .Options;

        var ctx = new ApplicationDbContext(contextOptions);
        ctx.Database.Migrate();

        return ctx;
    }
}