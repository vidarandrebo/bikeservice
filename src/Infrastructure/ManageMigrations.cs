using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Serilog;

namespace BikeService.Infrastructure;

public static class ManageMigrations
{
    public static async Task ApplyMigrations(this IServiceProvider serviceProvider, IWebHostEnvironment environment)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            if (environment.IsProduction())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                Log.Logger.Information("Running migration on database");
                var tries = 0;
                while (true)
                {
                    tries++;
                    try
                    {
                        db.Database.Migrate();
                        break;
                    }
                    catch (NpgsqlException)
                    {
                        Log.Logger.Warning("Database not accessible, trying again in 5 seconds");
                        await Task.Delay(5000);
                    }

                    if (tries > 5)
                    {
                        Log.Logger.Error("Migrations did not apply, continuing");
                        break;
                    }
                }
            }
            else
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                Log.Logger.Information("Running migration on database");
                db.Database.Migrate();
            }
        }
    }
}