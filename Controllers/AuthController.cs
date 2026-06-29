using Microsoft.AspNetCore.Mvc;
using ShadowTraceAPI.DTOs;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _authService.RegisterAsync(request);

        return Ok(new
        {
            Message = "User registered successfully."
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);

        return Ok(response);
    }
}