using FastEndpoints;
using FastEndpoints.Swagger;
using FastTest.Application.Querys;
using FastTest.Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDocument();

//Invocar string de conexion
builder.Services.AddDbContext<FastTestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetTasksQuery).Assembly));



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
app.UseFastEndpoints();
app.UseSwaggerGen();


app.Run();
