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
            DotEnv.Load(".env");
            var dbConnectionString = $"User ID={Environment.GetEnvironmentVariable("DB_USER")};" +
                                     $"Password={Environment.GetEnvironmentVariable("DB_PASSWD")};" +
                                     $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +
                                     $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                                     $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                                     $"Integrated Security=true;Pooling=true;";

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(dbConnectionString);
            });
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var folder = configuration.GetValue<string>("Database:Folder");
                var filename = configuration.GetValue<string>("Database:File");
                Directory.CreateDirectory(folder);
                options.UseSqlite($"Data Source={Path.Combine(folder, filename)}");
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
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