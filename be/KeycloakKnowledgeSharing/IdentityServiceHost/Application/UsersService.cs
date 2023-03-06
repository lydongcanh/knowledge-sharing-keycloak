using IdentityServiceHost.DTOs;
using IdentityServiceHost.Infrastructure;

namespace IdentityServiceHost.Application;

public class UsersService : IUsersService
{
    private readonly IKeycloakApis _keycloakApis;
    private readonly IProfileIndexRepository _profileIndexRepository;
    
    public UsersService(IKeycloakApis keycloakApis, IProfileIndexRepository profileIndexRepository)
    {
        _keycloakApis = keycloakApis;
        _profileIndexRepository = profileIndexRepository;
    }
    
    public async Task CreateUserAsync(KeycloakUser request)
    {
        var globalProfileId = Guid.NewGuid();

        const string globalProfileIdKey = "GlobalProfileId";
        if (!request.Attributes.ContainsKey(globalProfileIdKey))
        {
            request.Attributes.Add(globalProfileIdKey, globalProfileId.ToString());
        }
        
        await Task.WhenAll(
            _keycloakApis.CreateUserAsync(request),
            _profileIndexRepository.CreateProfileIndexAsync(new ProfileIndex(request.Email.ToUpperInvariant(), globalProfileId)));
    }

    public async Task<KeycloakUser?> GetUserByEmailAsync(string email)
    {
        var users = await _keycloakApis.GetUsersByEmailAsync(email, exact: true);
        return users.FirstOrDefault() ?? null;
    }
}