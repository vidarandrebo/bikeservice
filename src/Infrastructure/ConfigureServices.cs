using System;
using System.IO;
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

            var dbConnectionString = $"User ID={configuration.GetValue<string>("Database:User")};" +
                                     $"Password={configuration.GetValue<string>("Database:Password")};" +
                                     $"Server={configuration.GetValue<string>("Database:Server")};" +
                                     $"Port={configuration.GetValue<string>("Database:Port")};" +
                                     $"Database={configuration.GetValue<string>("Database:Name")};";

            services.AddDbContext<NpgsqlContext>((serviceProvider, options) =>
            {
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(dbConnectionString, x => x.MigrationsAssembly("BikeService.Migrations.Postgres"));
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
                options.UseSqlite($"Data Source={Path.Join(folder, filename)}",
                    sqliteOptions => { sqliteOptions.MigrationsAssembly("BikeService.Migrations.Sqlite"); });
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