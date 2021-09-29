using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BikeHistory.Services;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace BikeHistory
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

           //services.AddControllers()
           //    .AddNewtonsoftJson(options => options.UseMemberCasing());
            services.AddControllers();
            var path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "bike.db");
            services.AddDbContext<BikeContext>(options =>
            {
                options.UseSqlite($"Data Source={path}");
            });
            services.AddScoped<ITokenAuthenticator, TokenAuthenticator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IBikeProvider, BikeProvider>();
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BikeHistory", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BikeHistory v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
