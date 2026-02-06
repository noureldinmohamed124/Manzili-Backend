using Manzili.Api.Middlewares;
using Manzili.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



// 1. DbContext
builder.Services.AddDbContext<ManziliDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Home")));
















// Swagger (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RateLimitingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



/*Packages
 Install-Package Microsoft.EntityFrameworkCore -version 8.0.23 (API, Infrastructure)
 Install-Package Microsoft.EntityFrameworkCore.Tools -version 8.0.23 (API, Infrastructure)
 Install-Package Microsoft.EntityFrameworkCore.SqlServer -version 8.0.23 (API, Infrastructure)
 Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -version 8.0.23 (API)
 Install-Package Microsoft.AspNetCore.Cryptography.KeyDerivation -version 8.0.23 (Infrastructure) 
*/