using ShadowTraceAPI.DTOs;

namespace ShadowTraceAPI.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterRequest request);

    Task<LoginResponse> LoginAsync(LoginRequest request);
}