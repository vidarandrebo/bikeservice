using BikeHistory.Data;
using BikeHistory.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpLogging(options => { options.LoggingFields = HttpLoggingFields.All; });

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
builder.Services.AddDbContext<BikeContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));


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

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseDefaultFiles();

app.UseStaticFiles();

app.MapControllers();

app.Run();