using DevSphere.Domain.Entities;

namespace DevSphere.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(Guid userId);
    Task CreateUserAsync(User user);
}


