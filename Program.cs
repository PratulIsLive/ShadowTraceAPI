using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShadowTraceAPI.Data;
using ShadowTraceAPI.Interfaces;
using ShadowTraceAPI.Middleware;
using ShadowTraceAPI.Repositories;
using ShadowTraceAPI.Services;

var builder = WebApplication.CreateBuilder(args);


// Adding Services

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Database

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));
 

// Dependency Injection

    // Utilities
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<PasswordService>();
    builder.Services.AddScoped<JwtService>();
    builder.Services.AddScoped<ActivityLogger>();

    // Current User
    builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

    // Authentication
    builder.Services.AddScoped<IAuthService, AuthService>();

    // Case
    builder.Services.AddScoped<ICaseRepository, CaseRepository>();
    builder.Services.AddScoped<CaseService>();

    // Evidence
    builder.Services.AddScoped<IEvidenceRepository, EvidenceRepository>();
    builder.Services.AddScoped<IEvidenceService, EvidenceService>();

    // Suspect
    builder.Services.AddScoped<ISuspectRepository, SuspectRepository>();
    builder.Services.AddScoped<ISuspectService, SuspectService>();

    // Case-Suspect
    builder.Services.AddScoped<ICaseSuspectRepository, CaseSuspectRepository>();
    builder.Services.AddScoped<ICaseSuspectService, CaseSuspectService>();

    // Activity
    builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
    builder.Services.AddScoped<IActivityService, ActivityService>();

    // Dashboard
    builder.Services.AddScoped<IDashboardService, DashboardService>();


// JWT Authentication

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();


// Build Application

var app = builder.Build();


// Swagger

app.UseSwagger();
app.UseSwaggerUI();


// Middleware

app.UseMiddleware<GlobalExceptionMiddleware>();

// HTTPS only in Development

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();