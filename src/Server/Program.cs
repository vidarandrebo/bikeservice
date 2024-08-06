using System.Threading.Tasks;
using BikeService.Application;
using BikeService.Application.Interfaces;
using BikeService.Infrastructure;
using BikeService.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BikeService.Server;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.LoadEnvToConfiguration(".env");

        builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment);
        builder.Services.AddApplicationServices();


        builder.Services.AddRouting();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAuthentication();
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

        await app.Services.ApplyMigrations(app.Environment);

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
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