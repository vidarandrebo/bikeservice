using System;
using BikeService.Application.Interfaces;
using BikeService.Infrastructure.Identity;
using BikeService.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BikeService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        var dbConnectionString = configuration.GetValue<string>("ConnectionStrings:Default");

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(dbConnectionString);
        });


        services.AddSingleton<ITokenHandler, TokenHandler>();
        services.RegisterIdentity();
        return services;
    }
}