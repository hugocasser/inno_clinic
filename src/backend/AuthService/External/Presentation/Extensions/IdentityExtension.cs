using System.Text;
using Application.Options;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MicrosoftOptions = Microsoft.Extensions.Options.Options;

namespace Presentation.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .ConfigureIdentity()
            .AddJwtAuthentication(configuration);

        return services;
    }

    private static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, UserRole>(config =>
            {
                config.Password.RequiredLength = 8;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.User.RequireUniqueEmail = true;
                config.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders()
            .AddRoles<UserRole>();

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var tokenOptions = new AccessTokenOptions();
        configuration.GetSection(nameof(AccessTokenOptions)).Bind(tokenOptions);
        serviceCollection.AddSingleton(MicrosoftOptions.Create(tokenOptions));

        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => options
            .SetValidationTokenOptions(tokenOptions)
        );

        return serviceCollection;
    }

    private static JwtBearerOptions SetValidationTokenOptions(this JwtBearerOptions options,
        AccessTokenOptions accessTokenOptions)
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = accessTokenOptions.Issuer,
            ValidAudience = accessTokenOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenOptions.Key))
        };

        return options;
    }
}