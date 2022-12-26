using Application;
using Infrastructure;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();


builder.WebHost.UseUrls("http://localhost:5000");
builder.Services.AddCors(p =>
    p.AddPolicy("mypolicy",
        b => { b.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader(); }));
// Add services to the container.

builder.Services.AddRouting();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddSingleton<ITokenHandler, TokenHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();

app.UseCors("mypolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.MapFallbackToFile("index.html");

app.Run();