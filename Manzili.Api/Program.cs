using Manzili.Api.Middlewares;
using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.UseCases.Auth;
using Manzili.Application.UseCases.Services;
using Manzili.Infrastructure.Persistence;
using Manzili.Infrastructure.Repositories;
using Manzili.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



// 1. DbContext
builder.Services.AddDbContext<ManziliDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Home")));


// 2. Repositories
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
builder.Services.AddScoped<IServiceRepo, ServiceRepo>();


// 3. Services
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

builder.Services.AddLogging();


// 4. Use Cases
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();
builder.Services.AddScoped<RefreshTokenUseCase>();
builder.Services.AddScoped<GetAllServicesUseCase>();
builder.Services.AddScoped<GetServiceUseCase>();



// 5. JWT Configurations
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey!))
    };
});



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
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



/*Packages
 Install-Package Microsoft.EntityFrameworkCore -version 8.0.23 (Infrastructure)
 Install-Package Microsoft.EntityFrameworkCore.Tools -version 8.0.23 (Infrastructure)
 Install-Package Microsoft.EntityFrameworkCore.SqlServer -version 8.0.23 (Infrastructure)
 Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -version 8.0.23 (API)
 Install-Package Microsoft.AspNetCore.Cryptography.KeyDerivation -version 8.0.23 (Infrastructure) 
*/