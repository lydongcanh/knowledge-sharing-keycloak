namespace IdentityServiceHost.DTOs;

public record KeycloakCredential(string Value, string Type = "password");