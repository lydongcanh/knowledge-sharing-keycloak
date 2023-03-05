using IdentityServiceHost.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServiceHost.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(CreateUserRequest request)
    {
        return Ok();
    }
}