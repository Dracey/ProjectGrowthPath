using Microsoft.AspNetCore.Mvc;
using ProjectGrowthPath.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public AuthController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            await _identityService.SignInAsync(loginDto.Email, loginDto.Password);
            return Ok();
        }
        catch
        {
            return Unauthorized();
        }
    }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}