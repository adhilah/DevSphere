using System.Security.Claims;
using DevSphere.Application.Interfaces.Services;
using DevSphere.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace DevSphere.Application.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal User =>
        _httpContextAccessor.HttpContext?.User
        ?? throw new UnauthorizedAccessException("User not found.");

    public bool IsAuthenticated =>
        User.Identity?.IsAuthenticated ?? false;

    public Guid UserId
    {
        get
        {
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(value))
                throw new UnauthorizedAccessException("User Id not found.");

            return Guid.Parse(value);
        }
    }

    public UserRole Role
    {
        get
        {
            var value = User.FindFirstValue("role_id");

            if (string.IsNullOrWhiteSpace(value))
                throw new UnauthorizedAccessException("Role not found.");

            return (UserRole)int.Parse(value);
        }
    }
}