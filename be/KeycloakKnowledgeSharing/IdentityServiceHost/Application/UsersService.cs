using IdentityServiceHost.DTOs;
using IdentityServiceHost.Infrastructure;

namespace IdentityServiceHost.Application;

public class UsersService : IUsersService
{
    private readonly IKeycloakApis _keycloakApis;

    public UsersService(IKeycloakApis keycloakApis)
    {
        _keycloakApis = keycloakApis;
    }
    
    public async Task CreateUserAsync(KeycloakUser request)
    {
        const string globalProfileIdKey = "GlobalProfileId";
        if (!request.Attributes.ContainsKey(globalProfileIdKey))
        {
            request.Attributes.Add(globalProfileIdKey, Guid.NewGuid().ToString("N"));
        }
        
        await _keycloakApis.CreateUserAsync(request);
    }

    public async Task<KeycloakUser?> GetUserByEmailAsync(string email)
    {
        var users = await _keycloakApis.GetUsersByEmailAsync(email, exact: true);
        return users.FirstOrDefault() ?? null;
    }
}