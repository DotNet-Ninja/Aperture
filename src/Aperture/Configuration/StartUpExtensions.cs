using Aperture.Constants;
using Aperture.Controllers;
using Aperture.Data;
using Aperture.Services;
using Auth0.AspNetCore.Authentication;
using DotNetNinja.AutoBoundConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Aperture.Configuration;

public static class StartUpExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add application-specific services here
        services.AddHttpContextAccessor();
        services.AddSingleton<ITimeProvider, SystemTimeProvider>();
        services.AddSingleton<IErrorPageService, ErrorPageService>();
        services.AddScoped<IRepository, Repository>();
        services.AddScoped<INavigationService, NavigationService>();
        return services;
    }

    public static (Auth0Settings AuthSettings, AppSettings Settings, DatabaseSettings DbSettings) AddAutoBoundConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISignInService, SignInService>();
        var provider = 
        builder.Services.AddAutoBoundConfigurations(builder.Configuration).FromAssemblyOf<AppSettings>().Provider;
        return (provider.Get<Auth0Settings>(), provider.Get<AppSettings>(), provider.Get<DatabaseSettings>());
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

    public static IServiceCollection AddDataContext<TContext>(this IServiceCollection services, DatabaseSettings settings) where TContext : DbContext   
    {
        var name = typeof(TContext).Name;
        if(!settings.TryGetValue(name, out var config))
        {
            throw new ArgumentException($"Database settings for {name} not found.");
        }
        if(config.DbType == DbType.SqlServer)
        {
            services.AddDbContext<TContext>(options =>
                options.UseSqlServer(config.ConnectionString));
        }
        return services;
    }
}