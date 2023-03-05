using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Dto;

public record AccessToken(
    [property: JsonProperty("access_token")]
    [property: JsonPropertyName("access_token")] string Token,
    
    [property: JsonProperty("expires_in")]
    [property: JsonPropertyName("expires_in")] int? ExpiresIn,
    
    [property: JsonProperty("refresh_expires_in")]
    [property: JsonPropertyName("refresh_expires_in")] int? RefreshExpiresIn,
    
    [property: JsonProperty("token_type")]
    [property: JsonPropertyName("token_type")] string TokenType,
    
    [property: JsonProperty("not-before-policy")]
    [property: JsonPropertyName("not-before-policy")] int? NotBeforePolicy,
    
    [property: JsonProperty("scope")]
    [property: JsonPropertyName("scope")] string Scope
);
