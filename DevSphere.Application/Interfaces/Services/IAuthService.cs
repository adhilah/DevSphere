using DevSphere.Application.DTOs;
using DevSphere.Application.DTOs.Auth;

namespace DevSphere.Application.Interfaces.Services;
public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync(string token);
}

