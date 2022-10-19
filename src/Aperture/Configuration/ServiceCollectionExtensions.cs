using Aperture.Entities;
using Aperture.Entities.Migrations;
using Aperture.Services;
using Aperture.Services.Collectors;
using Auth0.AspNetCore.Authentication;
using DotNetNinja.AutoBoundConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Aperture.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddHttpContextAccessor()
            .AddTransient<ISignInService, SignInService>()
            .AddSingleton<IStorageProvider, AzureStorageProvider>()
            .AddScoped<ITimeProvider, DefaultTimeProvider>()
            .AddScoped<IExifToMetadataConverter, ExifToMetadataConverter>()
            .AddMetadataCollectors()
            .AddScoped<IPhotoService, PhotoService>();
    }

    public static IServiceCollection AddAutoBoundConfigurations
        (this IServiceCollection services, IConfiguration configuration, out IAutoBoundConfigurationProvider provider)
    {
        provider = services.AddAutoBoundConfigurations(configuration).FromAssemblyOf<StartUp>().Provider;
        return services;
    }

    public static IServiceCollection AddDataContext<TContext>(this IServiceCollection services, IAutoBoundConfigurationProvider provider) 
            where TContext : DbContext
    {
        string connectionName = typeof(TContext).Name;
        return services.AddDataContext<TContext>(provider, connectionName);
    }

    public static IServiceCollection AddDataContext<TContext>(this IServiceCollection services,
        IAutoBoundConfigurationProvider provider, string connectionName) where TContext : DbContext
    {
        var settings = provider.Get<DbSettings>();
        return services
            .AddDbContext<TContext>(options => options.UseSqlServer(settings.Contexts[connectionName].ConnectionString))
            .AddScoped<IDbMigrator<ApertureDb>, SqlDbMigrator<ApertureDb>>();
    }

    public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services, IAutoBoundConfigurationProvider provider)
    {
        var dbSettings = provider.Get<DbSettings>();
        var checks = services.AddHealthChecks();
        foreach (var db in dbSettings.Contexts)
        {
            checks = checks.AddSqlServer(db.Value.ConnectionString, name: db.Value.Name, tags: new[]
            {
                "Database",
                "SqlServer"
            });
        }

        return checks.Services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IAutoBoundConfigurationProvider provider)
    {
        var settings = provider.Get<AuthenticationSettings>();
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

       services.AddAuth0WebAppAuthentication(options => 
       {
                options.Domain = settings.Domain;
                options.ClientId = settings.ClientId;
       });
       return services;
    }

    private static IServiceCollection AddMetadataCollectors(this IServiceCollection services)
    {
        var collectorTypes = typeof(StartUp).Assembly.GetTypes()
            .Where(t => t.IsPublic && !t.IsAbstract && t.GetInterface(nameof(IMetadataCollector)) != null);
        foreach (var type in collectorTypes)
        {
            services.AddScoped(typeof(IMetadataCollector), type);
        }
        return services;
    }
}
