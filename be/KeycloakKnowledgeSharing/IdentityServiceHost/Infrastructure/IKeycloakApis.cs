using IdentityServiceHost.DTOs;
using Refit;

namespace IdentityServiceHost.Infrastructure;

[Headers("Authorization: Bearer")]
public interface IKeycloakApis
{
    [Get("/users")]
    Task<IEnumerable<KeycloakUser>> GetUsersByEmailAsync(string email, bool exact);
    
    [Post("/users")]
    Task CreateUserAsync(KeycloakUser request);
}