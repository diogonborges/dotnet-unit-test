using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.Api.Database;

namespace Movies.Api.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<Context>(
                    options =>
                    {
                        options.EnableSensitiveDataLogging();
                        options.UseInMemoryDatabase("TestDbName");
                        options.UseInternalServiceProvider(serviceProvider);
                    }
                );

                using var scope = services.BuildServiceProvider().CreateScope();
                var appDb = scope.ServiceProvider.GetRequiredService<Context>();
                appDb.Database.EnsureDeleted();
                appDb.Database.EnsureCreated();
            });
        }
    }
}