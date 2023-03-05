using Core.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Core.ServiceConfiguration;

public static class AuthConfigurations
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        const string domain = "http://localhost:8080/realms/dev";
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = domain;
            options.Audience = "account";
            options.RequireHttpsMetadata = false;
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("be00:master", policy => policy.Requirements.Add(new HasScopeRequirement("be00:master", domain)));
        });
            
        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        return services;
    }
}