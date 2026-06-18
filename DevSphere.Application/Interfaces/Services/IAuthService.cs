using DevSphere.Application.DTOs.Auth;
using DevSphere.Domain.Entities;

namespace DevSphere.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
};

