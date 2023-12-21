using Application.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        if (environment.IsProduction())
        {
            Console.WriteLine("Production");
            DotEnv.Load(".env");
            var dbConnectionString = $"User ID={Environment.GetEnvironmentVariable("DB_USER")};" +
                                     $"Password={Environment.GetEnvironmentVariable("DB_PASSWD")};" +
                                     $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +
                                     $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                                     $"Database={Environment.GetEnvironmentVariable("DB_NAME")};";

            services.AddDbContext<NpgsqlContext>((serviceProvider, options) =>
            {
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(dbConnectionString);
            });
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<NpgsqlContext>());
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<NpgsqlContext>()
                .AddUserManager<UserManager<User>>();
        }
        else
        {
            services.AddDbContext<SqliteContext>((serviceProvider, options) =>
            {
                var folder = configuration.GetValue<string>("Database:Folder");
                var filename = configuration.GetValue<string>("Database:File");
                Directory.CreateDirectory(folder);
                options.UseSqlite($"Data Source={Path.Combine(folder, filename)}");
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            });
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<SqliteContext>());
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<SqliteContext>()
                .AddUserManager<UserManager<User>>();
        }

        services.AddTransient<IIdentityService, IdentityService>();
        return services;
    }
}