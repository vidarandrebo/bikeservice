using System;
using System.Text;
using System.Threading.Tasks;
using BikeService.Application;
using BikeService.Application.Parts.EventHandlers;
using BikeService.Domain.Bikes.Events;
using BikeService.EventBus;
using BikeService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace BikeService.Server;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();

        builder.Services.AddEventBus(builder.Configuration);

        builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment);
        builder.Services.AddApplicationServices();

        builder.Services.AddRouting();
        builder.Services.AddControllers();
        //builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:ValidAudience"],
                    ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        builder.Configuration.GetValue<string>("Jwt:Secret") ??
                        throw new Exception("No Jwt Secret provided"))),
                };
            });
        builder.Services.AddAuthorization();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.File(builder.Configuration["Serilog:LogFile"] ?? "log", rollOnFileSizeLimit: true)
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var eventBus = scope.ServiceProvider.GetRequiredService<IEventBusClient>();
            eventBus.AddEventHandler<BikeMileageUpdatedEvent, BikeMileageUpdatedEventHandler>();
        }

        await app.Services.ApplyMigrations(app.Environment);

        if (app.Environment.IsDevelopment())
        {
            Console.WriteLine("error");
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });
        }

        app.UseRouting();

        app.UseAuthentication();


        app.UseDefaultFiles();

        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.Headers.Append(
                    "Cache-Control", $"public, max-age={app.Configuration.GetValue<int>("CacheMaxAge")}");
            }
        });

        app.UseAuthorization();

        app.MapControllers();

        app.MapFallbackToFile("index.html");

        app.Run();
    }
}