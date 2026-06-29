using Microsoft.EntityFrameworkCore;
using ShadowTraceAPI.Data;
using ShadowTraceAPI.DTOs;
using ShadowTraceAPI.Entities;
using ShadowTraceAPI.Enums;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwordService;
    private readonly JwtService _jwtService;

    public AuthService(
        AppDbContext context,
        PasswordService passwordService,
        JwtService jwtService)
    {
        _context = context;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    // Register method
    public async Task RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (existingUser is not null)
        {
            throw new Exception("Email already exists.");
        }

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = _passwordService.HashPassword(request.Password),
            Role = Role.Investigator,
            IsActive = true
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    // Login method
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user is null)
        {
            throw new Exception("Invalid email or password.");
        }

        if (!user.IsActive)
        {
            throw new Exception("User account is inactive.");
        }

        var isPasswordValid = _passwordService.VerifyPassword(
            request.Password,
            user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new Exception("Invalid email or password.");
        }

        var token = _jwtService.GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
}