using DevSphere.Domain.Enums;
namespace DevSphere.Application.Interfaces.Services;

public interface ICurrentUserService
{
    Guid UserId { get; }

    UserRole Role { get; }

    bool IsAuthenticated { get; }
}