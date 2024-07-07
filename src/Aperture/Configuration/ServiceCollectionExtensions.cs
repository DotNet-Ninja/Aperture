using Aperture.Services;
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
        //.AddScoped<IDbMigrator<ApertureDb>, SqlDbMigrator<ApertureDb>>()
            .AddSingleton<ITimeProvider, SystemTimeProvider>();
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
        //var settings = provider.Get<DbSettings>();
        //return services
        //    .AddDbContext<TContext>(options => options.UseSqlServer(settings.Contexts[connectionName].ConnectionString))
        //    //.AddScoped<IDbMigrator, SqlDbMigrator>()
        //    ;
        return services;
    }

    public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services, IAutoBoundConfigurationProvider provider)
    {
        //var dbSettings = provider.Get<DbSettings>();
        var checks = services.AddHealthChecks();
        //foreach (var db in dbSettings.Contexts)
        //{
        //    checks = checks.AddSqlServer(db.Value.ConnectionString, name: db.Value.Name, tags: new[]
        //    {
        //        "Database",
        //        "SqlServer"
        //    });
        //}

        //return checks.Services;
        return services;
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

}