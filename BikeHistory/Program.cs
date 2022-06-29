using BikeHistory;
using BikeHistory.Data;
using BikeHistory.Models.Auth;
using BikeHistory.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(p => p.AddPolicy("mypolicy", b =>
{
    b.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader();
}));
// Add services to the container.

builder.Services.AddRouting();
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


builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<BikeContext>()
    .AddUserManager<UserManager<User>>();

builder.Services.AddSingleton<ITokenHandler, TokenHandler>();

builder.Services.AddAuthentication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("mypolicy");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.MapFallbackToFile("index.html");

app.Run();