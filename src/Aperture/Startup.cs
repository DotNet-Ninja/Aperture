using Aperture.Configuration;

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
            //.AddAuthConfiguration(settings)
            
            .AddApplicationHealthChecks(settings)
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
    }
}