using Aperture.Configuration;
using DotNetNinja.AutoBoundConfiguration;

namespace Aperture;

public class StartUp(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoBoundConfigurations(configuration, out var settings)
            //.AddDataContext<ApertureDb>(settings)
            .AddApplicationHealthChecks(settings)
            .AddAuthentication(settings)
            .AddApplicationServices()
            .AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseStrictTransportSecurity(env)
            .UseGlobalExceptionHandler(env)
            .UseHttpsRedirection()
            .UseStaticFiles()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseApplicationEndpoints();

        //migrator.Migrate().SeedData();
    }
}