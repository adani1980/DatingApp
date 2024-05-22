using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// This is within Extensions/ApplicationServiceExtensions
builder.Services.AddApplicationServices(builder.Configuration);
// This is within Extensions/IdentityServiceExtensions
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline

app.UseHttpsRedirection();

// Allowing client from https://localhost:4200 to make a request to the API
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

// Adding the Middleware for the JWT Token
// !!! Must be before app.MapControllers(); !!!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
