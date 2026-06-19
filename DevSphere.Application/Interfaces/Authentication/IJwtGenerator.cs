using DevSphere.Domain.Entities;

namespace DevSphere.Application.Interfaces.Authentication;

public interface IJwtGenerator
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}