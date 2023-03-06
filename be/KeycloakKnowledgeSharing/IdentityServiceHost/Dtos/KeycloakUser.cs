using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace IdentityServiceHost.DTOs;

public class KeycloakUser
{
    [JsonProperty("email")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = "";

    [JsonProperty("username")]
    [JsonPropertyName("username")]
    public string Username { get; set; } = "";

    [JsonProperty("firstName")]
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = ""; 

    [JsonProperty("lastName")]
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = "";

    [JsonProperty("enabled")]
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = true;

    [JsonProperty("emailVerified")]
    [JsonPropertyName("emailVerified")]
    public bool EmailVerified { get; set; } = true;
    
    [JsonProperty("credentials")]
    [JsonPropertyName("credentials")]
    public IList<KeycloakCredential> Credentials { get; } = new List<KeycloakCredential>();
    
    [JsonProperty("attributes")]
    [JsonPropertyName("attributes")]
    public IDictionary<string, string> Attributes { get; } = new Dictionary<string, string>();

}