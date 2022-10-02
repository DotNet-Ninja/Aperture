using Aperture.Configuration;
using Aperture.Entities;
using Aperture.Entities.Migrations;

namespace Aperture;

public class StartUp
{
    public IConfiguration Configuration { get; }

    public StartUp(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoBoundConfigurations(Configuration, out var settings)
            .AddDataContext<ApertureDb>(settings)
            .AddApplicationHealthChecks(settings)
            .AddAuthentication(settings)
            .AddApplicationServices()
            .AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbMigrator<ApertureDb> migrator)
    {
        app.UseStrictTransportSecurity(env)
            .UseGlobalExceptionHandler(env)
            .UseHttpsRedirection()
            .UseStaticFiles()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseApplicationEndpoints();

        migrator.Migrate().SeedData();
    }
}