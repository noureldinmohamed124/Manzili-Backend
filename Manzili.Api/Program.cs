using Manzili.Api.Middlewares;
using Manzili.Application.Abstractions.FileStorage;
using Manzili.Application.Abstractions.Persistence;
using Manzili.Application.Abstractions.Security;
using Manzili.Application.Admin.Dashboard;
using Manzili.Application.Admin.Financials.Queries;
using Manzili.Application.Admin.Financials.UseCases;
using Manzili.Application.Admin.Orders.UseCases;
using Manzili.Application.Admin.Payments.UseCases;
using Manzili.Application.Admin.Services.UseCases;
using Manzili.Application.Admin.Users.UseCases;
using Manzili.Application.Auth.UseCases;
using Manzili.Application.Buyer.UseCases.Orders;
using Manzili.Application.Buyer.UseCases.Services;
using Manzili.Application.Common.Interfaces;
using Manzili.Application.Seller.UseCases;
using Manzili.Application.Shared.UseCases;
using Manzili.Infrastructure.FileStorage;
using Manzili.Infrastructure.Persistence;
using Manzili.Infrastructure.Repositories;
using Manzili.Infrastructure.Security;
using Manzili.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



// 1. DbContext
builder.Services.AddDbContext<ManziliDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CleanAsp")));
// CleanAsp
// AspTest


// 2. Repositories
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
builder.Services.AddScoped<IServiceRepo, ServiceRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IServiceOptionRepo, ServiceOptionRepo>();
builder.Services.AddScoped<IAddressRepo, AddressRepo>();
builder.Services.AddScoped<IPaymentProofRepo, PaymentProofRepo>();
builder.Services.AddScoped<ISellerRepo, SellerRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IAdminRepo, AdminRepo>();



// 3. Services
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddScoped<ITransactionCodeGenerator, TransactionCodeGenerator>();

builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();

builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();



// 4. Use Cases
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();
builder.Services.AddScoped<RefreshTokenUseCase>();
// Categories
builder.Services.AddScoped<GetAllCategoriesUseCase>();
// Services
builder.Services.AddScoped<GetAllServicesUseCase>();
builder.Services.AddScoped<GetServiceByIdUseCase>();
builder.Services.AddScoped<GetHomeSectionUseCase>();
builder.Services.AddScoped<SearchServicesUseCase>();
// Orders
builder.Services.AddScoped<RequestServiceUseCase>();
builder.Services.AddScoped<GetAllOrdersUseCase>();
builder.Services.AddScoped<GetPaymentSummaryUseCase>();
builder.Services.AddScoped<SubmitPaymentUseCase>();
// Seller Actions
builder.Services.AddScoped<GetDashboardStatsUseCase>();
builder.Services.AddScoped<GetSellerServicesUseCase>();
builder.Services.AddScoped<GetSellerServiceByIdUseCase>();
builder.Services.AddScoped<CreateServiceUseCase>();
builder.Services.AddScoped<UpdateServiceUseCase>();
builder.Services.AddScoped<DeleteServiceUseCase>();
builder.Services.AddScoped<GetSellerOrdersUseCase>();
builder.Services.AddScoped<GetSellerOrderByIdUseCase>();
builder.Services.AddScoped<ApproveOrderUseCase>();
builder.Services.AddScoped<RejectOrderUseCase>();
builder.Services.AddScoped<RepriceOrderUseCase>();
builder.Services.AddScoped<UpdateOrderStatusUseCase>();
// Admin
builder.Services.AddScoped<GetAdminDashboardStatsUseCase>();
builder.Services.AddScoped<GetAdminAllUsersUseCase>();
builder.Services.AddScoped<GetAdminUserDetailsUseCase>();
builder.Services.AddScoped<BlockUserUseCase>();
builder.Services.AddScoped<UnblockUserUseCase>();
builder.Services.AddScoped<GetAdminServicesUseCase>();
builder.Services.AddScoped<GetAdminOrdersUseCase>();
builder.Services.AddScoped<GetAdminFinancialsUseCase>();
builder.Services.AddScoped<GetPaymentRequestsUseCase>();
builder.Services.AddScoped<ApprovePaymentUseCase>();
builder.Services.AddScoped<RejectPaymentProofUseCase>();



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


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:3000")
            .AllowAnyMethod().AllowAnyHeader();
    });
});




// force lowercase URLs
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

// When converting enums to/from JSON → use names instead of numbers
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter());
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

app.UseCors("AllowFrontend");

//app.UseMiddleware<RateLimitingMiddleware>();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



/*Packages
 Install-Package Microsoft.EntityFrameworkCore -version 8.0.23 (Infrastructure)
 Install-Package Microsoft.EntityFrameworkCore.Tools -version 8.0.23 (Infrastructure)
 Install-Package Microsoft.EntityFrameworkCore.SqlServer -version 8.0.23 (Infrastructure)
 Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -version 8.0.23 (API)
 Install-Package Microsoft.EntityFrameworkCore.Design -version 8.0.23 (API)
 Install-Package Microsoft.AspNetCore.Cryptography.KeyDerivation -version 8.0.23 (Infrastructure)


 Add-Migration InitialCreate -OutputDir Data/Migrations
*/