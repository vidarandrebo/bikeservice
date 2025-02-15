using System;
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
        using (var scope = serviceProvider.CreateScope())
        {
            if (environment.IsDevelopment())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                Log.Logger.Information("Running migration on database");
                db.Database.Migrate();
            }
        }
    }
}