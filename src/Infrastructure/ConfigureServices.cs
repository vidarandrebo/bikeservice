using Application.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        if (environment.IsProduction())
        {
            DotEnv.Load(".env");
            var dbConnectionString = $"User ID={Environment.GetEnvironmentVariable("DB_USER")};" +
                                     $"Password={Environment.GetEnvironmentVariable("DB_PASSWD")};" +
                                     $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +
                                     $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                                     $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                                     $"Integrated Security=true;Pooling=true;";

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(dbConnectionString));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var folder = configuration.GetValue<string>("Database:Folder");
                var filename = configuration.GetValue<string>("Database:File");
                Directory.CreateDirectory(folder);
                options.UseSqlite($"Data Source={Path.Combine(folder, filename)}");
            });
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<UserManager<User>>();
        services.AddTransient<IIdentityService, IdentityService>();
        return services;
    }
}