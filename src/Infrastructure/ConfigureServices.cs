using System;
using System.IO;
using BikeService.Application.Interfaces;
using BikeService.Infrastructure.Identity;
using BikeService.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BikeService.Infrastructure;

public static class DependencyInjection
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

            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((serviceProvider, options) =>
            {
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(dbConnectionString,
                    postgresOptions => { postgresOptions.MigrationsAssembly("BikeService.Migrations.Postgres"); });
            });
        }
        else
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((serviceProvider, options) =>
            {
                var folder = configuration.GetValue<string>("Database:Folder");
                var filename = configuration.GetValue<string>("Database:File");
                options.UseSqlite($"Data Source={Path.Join(folder, filename)}",
                    sqliteOptions => { sqliteOptions.MigrationsAssembly("BikeService.Migrations.Sqlite"); });
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            });
        }


        services.AddSingleton<ITokenHandler, TokenHandler>();
        services.RegisterIdentity();
        return services;
    }
}