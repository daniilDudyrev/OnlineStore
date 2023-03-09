using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OnlineStore.Data;

namespace OnlineStore.WebApi.IntegrationTests;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    private const string DbPath = "test.db";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<AppDbContext>();
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlite($"Data Source={DbPath}"));
            using var serviceProvider = services.BuildServiceProvider();
            using var context = serviceProvider.GetService<AppDbContext>();
            context!.Database.EnsureCreated();
        });
    }

    public override async ValueTask DisposeAsync()
    {
        using (var serviceScope = Server.Services.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            await context!.Database.EnsureDeletedAsync();
        }
        await base.DisposeAsync();
    }
}