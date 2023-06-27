using Application.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        /*
        builder.Services.AddDbContext<BikeContext>(options =>
        {
            options.UseSqlite($"Data Source={Path.Combine("Data", "bike.db")}");
        });
        */
       DotEnv.Load(".env");
       var dbConnectionString = $"User ID={Environment.GetEnvironmentVariable("DB_USER")};" +
                                $"Password={Environment.GetEnvironmentVariable("DB_PASSWD")};" +
                                $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +
                                $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                                $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                                $"Integrated Security=true;Pooling=true;";
    
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(dbConnectionString));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<UserManager<User>>();
        services.AddTransient<IIdentityService, IdentityService>();
        return services;
    }
}