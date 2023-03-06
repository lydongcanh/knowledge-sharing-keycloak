using IdentityServiceHost.Application;
using IdentityServiceHost.Infrastructure;
using Refit;
using Shared.Dto;
using Shared.ServiceConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Services
var services = builder.Services;

services.AddRouting();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddAuth();
services.AddSwagger();

services.AddSingleton(_ => RestService.For<IKeycloakApis>("http://localhost:8080/admin/realms/dev", new RefitSettings
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
services.AddSingleton<IProfileIndexRepository>(_ =>
    new ProfileIndexRepository(builder.Configuration.GetConnectionString("IdentityConnection")));

// App
var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(routeBuilder => routeBuilder.MapControllers());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
