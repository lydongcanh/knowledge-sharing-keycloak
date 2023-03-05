using IdentityServiceHost.Application;
using IdentityServiceHost.Infrastructure;
using Refit;
using Shared.Dto;
using Shared.ServiceConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Services
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwagger();
services.AddAuth();

services.AddSingleton(_ => RestService.For<IKeycloakApis>("http://localhost:8080/admin/realms/dev", new RefitSettings()
{
    AuthorizationHeaderValueGetter = async () =>
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
    
        return (await response.Content.ReadFromJsonAsync<AccessToken>())?.Token;
    }
}));

services.AddSingleton<IUsersService, UsersService>();

// App
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
