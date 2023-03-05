using IdentityServiceHost.DTOs;

namespace IdentityServiceHost.Application;

public interface IUsersService
{
    Task CreateUserAsync(KeycloakUser request);
    
    Task<KeycloakUser?> GetUserByEmailAsync(string email);
}