using Aperture.Constants;

namespace Aperture.Configuration;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApplicationEndpoints(this IApplicationBuilder app)
    {
        return app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks(WellKnownEndpoint.HealthChecks.Liveliness, CustomHealthCheckOptions.LivelinessOptions);
            endpoints.MapHealthChecks(WellKnownEndpoint.HealthChecks.Databases, CustomHealthCheckOptions.TaggedDefaultOptions("Database"));
            endpoints.MapHealthChecks(WellKnownEndpoint.HealthChecks.Readiness, CustomHealthCheckOptions.Default);
            endpoints.MapControllerRoute(
                name: "Default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }

    public static IApplicationBuilder UseStrictTransportSecurity(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseHsts();
        }
        return app;
    }

    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            return app.UseDeveloperExceptionPage();
        }

        return app.UseExceptionHandler(WellKnownEndpoint.ErrorHandler);
    }
}