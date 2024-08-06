using System;
using BikeService.Application.Interfaces;
using BikeService.Infrastructure.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BikeService.Infrastructure;

public static class Register
{
    public static IServiceCollection RegisterIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<Guid>>(
                options => { options.User.RequireUniqueEmail = true; }
            )
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();
        services.AddTransient<IIdentityService, IdentityService>();
        return services;
    }
}