using FastEndpoints;
using FastEndpoints.Swagger;
using FastTest.Application.Querys;
using FastTest.Infraestructure.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDocument();

//Invocar string de conexion
builder.Services.AddDbContext<FastTestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetTasksQuery).Assembly));

//Seguridad Jwt
var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };
    });

//Agregar Rate limite para evitar ataques ddos 
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("api", config =>
    {
        config.PermitLimit = 100;
        config.Window = TimeSpan.FromMinutes(1);
        config.QueueLimit = 10;
    });
});

builder.Services.AddAuthorization();

//Agregar logs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

//Crear BD al iniciar
await using (AsyncServiceScope scope = app.Services.CreateAsyncScope())
{
    FastTestDbContext dbContext = scope.ServiceProvider.GetRequiredService<FastTestDbContext>();
    IEnumerable<string> pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

    if (pendingMigrations.Any())
    {
        await dbContext.Database.MigrateAsync();
    }
}


app.UseOpenApi();

app.UseSwaggerGen();
//seguridad
app.UseAuthentication();
app.UseAuthorization();
//RateLimite
app.UseRateLimiter();
//errores genericos
app.UseExceptionHandler("/error");



app.UseFastEndpoints();

app.Run();
