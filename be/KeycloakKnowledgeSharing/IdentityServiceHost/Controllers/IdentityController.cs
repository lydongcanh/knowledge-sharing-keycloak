using Microsoft.AspNetCore.Mvc;
using Shared.Dto;

namespace IdentityServiceHost.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    [HttpGet("/access-token")]
    public async Task<IActionResult> GetAccessTokenAsync()
    {
        const string url = "http://localhost:8080/realms/dev/protocol/openid-connect/token";
        var data = new Dictionary<string, string>
        {
            { "client_id", "identity-services" },
            { "client_secret", "e5FnXA1BxgZS0mp5TIZVg1TUjFYpsEk6" },
            { "grant_type", "client_credentials" }
        };
    
        using var client = new HttpClient();
        var response = await client.PostAsync(url, new FormUrlEncodedContent(data));
    
        return Ok(await response.Content.ReadFromJsonAsync<AccessToken>());;
    }
}
