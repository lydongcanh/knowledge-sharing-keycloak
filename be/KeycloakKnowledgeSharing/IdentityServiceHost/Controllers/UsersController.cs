using IdentityServiceHost.Application;
using IdentityServiceHost.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServiceHost.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(KeycloakUser request)
    {
        await _usersService.CreateUserAsync(request);
        return StatusCode(201);
    }
}