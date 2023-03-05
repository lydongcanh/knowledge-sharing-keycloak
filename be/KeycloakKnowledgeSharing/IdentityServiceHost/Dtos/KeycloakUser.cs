namespace IdentityServiceHost.DTOs;

public record KeycloakUser(
    string Email,
    string Username,
    string FirstName,
    string LastName,
    IReadOnlyList<KeycloakCredential> Credentials,
    IDictionary<string, string> Attributes,
    bool Enabled = true,
    bool EmailVerified = true);