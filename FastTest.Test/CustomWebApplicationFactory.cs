using FastTest.Application.Commands;
using FastTest.Infraestructure.Contexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FastTest.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // no usar  sqlServer
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<FastTestDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // levantar bdmemoria
                services.AddDbContext<FastTestDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // quitar auth real
                services.RemoveAll(typeof(IAuthenticationSchemeProvider));
                services.RemoveAll(typeof(IAuthenticationHandlerProvider));
                services.RemoveAll(typeof(IAuthenticationService));

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                })
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });



                using var scope = services.BuildServiceProvider().CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<FastTestDbContext>();

                db.Database.EnsureCreated();
                db.TaskItems.AddRange(
                    new Core.FastTest.Domain.TaskItem
                    {
                        Id = Guid.NewGuid(),
                        Title = "Tarea inicial",
                        Description = "Sembrada para pruebas",
                        Status = Core.FastTest.Domain.TaskStatus.Pending,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Core.FastTest.Domain.TaskItem
                    {
                        Id = Guid.NewGuid(),
                        Title = "Otra tarea",
                        Description = "Sembrada para pruebas",
                        Status = Core.FastTest.Domain.TaskStatus.Completed,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );

                db.SaveChanges();
            });

            builder.UseEnvironment("Testing");
        }
    }
}
