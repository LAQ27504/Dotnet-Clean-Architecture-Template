//#if (isAuthen)
using System.Security.Claims;
using apiclean.Application.Services.Auth;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using apiclean.Application.Interfaces.Services.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
//#endif
using apiclean.Application.Interfaces.Services;
using apiclean.Application.Interfaces.UnitOfWork;
using apiclean.Application.Services;
using apiclean.Infrastructure;
using apiclean.Infrastructure.Persistence;
using apiclean.Application.Interfaces.Repositories;
using apiclean.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database configuration
//#if (db == "SQLite")
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//#elif (db == "SQLServer")
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//#elif (db == "PostgreSQL")
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
//#endif

//#if (isAuthen)
//Authentication
var configuration = builder.Configuration;

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JwtSettings:Issuer"],
            ValidAudience = configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    configuration["JwtSettings:SecretKey"] ?? "hungprono1isthepasskey"
                )
            ),
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = ClaimTypes.Role,
        };
    });
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        { 
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGci...\"",
        }
    );

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                new string[] { }
            },
        }
    );
});
//#endif

// Dependency Injection
// 1. Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 2. Repositories
builder.Services.AddScoped<IEgRepository, EgRepository>();

//#if (isAuthen)
builder.Services.AddScoped<IUserRepository, UserRepository>();

//#endif

// 3. Services
builder.Services.AddScoped<IEgService, EgService>();

//#if (isAuthen)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

//#endif

// Add services to the container.
builder.Services.AddOpenApi();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
//#if (isAuthen)
app.UseAuthorization();
app.UseAuthorization();
//#endif
app.MapControllers();

app.Run();
