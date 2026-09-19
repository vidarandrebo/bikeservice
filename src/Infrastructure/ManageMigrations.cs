using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BikeService.Infrastructure;

public static class ManageMigrations
{
    public static async Task ApplyMigrations(this IServiceProvider serviceProvider, IWebHostEnvironment environment)
    {
        if (Assembly.GetEntryAssembly()?.GetName().Name != "GetDocument.Insider")
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                Log.Logger.Information("Running migration on database");
                try
                {
                    await db.Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Error running migrations");
                    throw;
                }
            }
        }
    }
}