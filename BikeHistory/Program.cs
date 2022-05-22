using BikeHistory;
using BikeHistory.Data;
using BikeHistory.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program));
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
builder.Services.AddDbContext<BikeContext>(options =>
    options.UseNpgsql(dbConnectionString));


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<BikeContext>()
    .AddUserManager<UserManager<User>>();

builder.Services.AddAuthentication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseDefaultFiles();

app.UseStaticFiles();

app.MapControllers();

app.Run();