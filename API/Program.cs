using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// This will make posible the request from the client
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline

app.UseHttpsRedirection();

app.UseAuthorization();

// Allowing client from https://localhost:4200 to make a request to the API
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.MapControllers();

app.Run();
