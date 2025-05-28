using Ambev.DeveloperEvaluation.ORM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Ambev.DeveloperEvaluation.WebApi;

namespace Ambev.DeveloperEvaluation.Integration.Common;

/// <summary>
/// Custom WebApplicationFactory for integration tests.
/// Overrides the database context to use an in-memory database.
/// </summary>
public abstract class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    /// <inheritdoc />
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the app's DbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DefaultContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            // Add DbContext using in-memory provider for testing.
            services.AddDbContext<DefaultContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Ensure the database is created.
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            db.Database.EnsureCreated();
        });

        return base.CreateHost(builder);
    }
}