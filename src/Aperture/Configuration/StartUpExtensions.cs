using Aperture.Controllers;
using Aperture.Services;
using Auth0.AspNetCore.Authentication;
using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

public static class StartUpExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add application-specific services here
        services.AddHttpContextAccessor();
        services.AddSingleton<ITimeProvider, SystemTimeProvider>();
        return services;
    }

    public static (Auth0Settings AuthSettings, AppSettings Settings) AddAutoBoundConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISignInService, SignInService>();
        var provider = 
        builder.Services.AddAutoBoundConfigurations(builder.Configuration).FromAssemblyOf<AppSettings>().Provider;
        return (provider.Get<Auth0Settings>(), provider.Get<AppSettings>());
    }

    public static IServiceCollection WithContext(this IMvcBuilder builder)
    {
        builder.Services
            .AddScoped(typeof(IMvcContext<>), typeof(MvcContext<>));
        return builder.Services;
    }

    public static IServiceCollection AddAuth0Authentication(this IServiceCollection services, Auth0Settings auth0)
    {
        services.AddAuth0WebAppAuthentication(options =>
        {
            options.Domain = auth0.Domain;
            options.ClientId = auth0.ClientId;
            options.ClientSecret = auth0.ClientSecret;
        });
        return services;
    }
}