using FastEndpoints;
using FastEndpoints.Swagger;
using FastTest.Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDocument();

builder.Services.AddDbContext<FastTestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

await using (AsyncServiceScope scope = app.Services.CreateAsyncScope())
{
    FastTestDbContext dbContext = scope.ServiceProvider.GetRequiredService<FastTestDbContext>();
    IEnumerable<string> pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

    if (pendingMigrations.Any())
    {
        await dbContext.Database.MigrateAsync();
    }
}

app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi();


app.Run();
