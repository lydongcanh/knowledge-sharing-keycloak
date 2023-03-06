using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Shared.Authorization;

namespace Shared.ServiceConfiguration;

public static class AuthConfigurations
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        const string domain = "http://localhost:8080/realms/dev";
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.Authority = domain;
            options.RequireHttpsMetadata = false;
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("identity:users:create", policy => policy.Requirements.Add(new HasScopeRequirement("identity:users:create", domain)));
        });
            
        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        return services;
    }
}