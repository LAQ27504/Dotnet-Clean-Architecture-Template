//#if (isAuthen)
using Microsoft.AspNetCore.Authentication.Cookies;
using mudblazorclean.Application.Interfaces.Services.Authentication;
using mudblazorclean.Application.Services.Auth;
//#endif
using MudBlazor.Services;
using mudblazorclean.Application.Interfaces.Repositories;
using mudblazorclean.Application.Interfaces.Services;
using mudblazorclean.Application.Interfaces.UnitOfWork;
using mudblazorclean.Application.Services;
using mudblazorclean.Infrastructure;
using mudblazorclean.Infrastructure.Persistence;
using mudblazorclean.Infrastructure.Repositories;
using mudblazorclean.Presentation.Components;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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

// set up authen
//#if (isAuthen)
// --- 2. Add Authentication & Authorization Services ---
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAntiforgery();

builder
    .Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/auth/login";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/access_denied";
    });

builder.Services.AddAuthorization();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

//#if (isAuthen)
app.UseAuthorization();
app.UseAuthorization();
//#endif

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
