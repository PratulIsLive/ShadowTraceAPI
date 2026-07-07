using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ShadowTraceAPI.Extensions;
using ShadowTraceAPI.Interfaces;

namespace ShadowTraceAPI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null)
                throw new UnauthorizedAccessException("No authenticated user.");

            Console.WriteLine("========== Current User Debug ==========");

            Console.WriteLine($"Authenticated: {user.Identity?.IsAuthenticated}");
            Console.WriteLine($"Authentication Type: {user.Identity?.AuthenticationType}");
            Console.WriteLine($"User Name: {user.Identity?.Name}");

            Console.WriteLine("Claims:");

            foreach (var claim in user.Claims)
            {
                Console.WriteLine($"{claim.Type} = {claim.Value}");
            }

            Console.WriteLine("========================================");

            return user.GetUserId();
        }
    }
}