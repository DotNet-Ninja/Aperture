using System.Text;
using Aperture.Constants;
using Aperture.Controllers;
using Aperture.Data;
using Aperture.Services;
using DotNetNinja.AutoBoundConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        services.AddSingleton<ITokenService, TokenService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
        services.AddScoped<IAvatarService, GravatarService>();
        return services;
    }

    public static (JwtOptions AuthSettings, AppSettings Settings, DatabaseSettings DbSettings) AddAutoBoundConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISignInService, SignInService>();
        var provider = 
        builder.Services.AddAutoBoundConfigurations(builder.Configuration).FromAssemblyOf<AppSettings>().Provider;
        return (provider.Get<JwtOptions>(), provider.Get<AppSettings>(), provider.Get<DatabaseSettings>());
    }

    public static IServiceCollection WithContext(this IMvcBuilder builder)
    {
        builder.Services
            .AddScoped(typeof(IMvcContext<>), typeof(MvcContext<>));
        return builder.Services;
    }

    public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (string.IsNullOrWhiteSpace(context.Token) &&
                            context.Request.Cookies.TryGetValue(JwtCookie.Name, out var token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
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