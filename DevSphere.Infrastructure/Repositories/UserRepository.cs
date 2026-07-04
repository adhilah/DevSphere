using Dapper;
using DevSphere.Application.Interfaces.Infrastructure;
using DevSphere.Application.Interfaces.Repositories;
using DevSphere.Domain.Entities;
using DevSphere.Infrastructure.Data;

namespace DevSphere.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDapperContext _context;

    public UserRepository(IDapperContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM fn_get_user_by_id(@Id)",
            new { Id = userId });
    }
    public async Task<User?> GetByUsernameAsync(string username)
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM fn_get_user_by_username(@Username)",
            new { Username = username });
    }
    public async Task<User?> GetByEmailAsync(string email)
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM fn_get_user_by_email(@Email)",
            new { Email = email });
    }

    public async Task AddAsync(User user)
    {
        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(
            @"CALL sp_add_user(
            @Id,
            @Username,
            @Email,
            @PasswordHash,
            @RoleId,
            @CreatedAt,
            @UpdatedAt
        )",
            new
            {
                user.Id,
                user.Username,
                user.Email,
                user.PasswordHash,
                user.RoleId,
                user.CreatedAt,
                user.UpdatedAt
            });
    }
    public async Task UpdateAsync(User user)
    {
        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(
            @"CALL sp_update_user(
            @Id,
            @Username,
            @Email,
            @PasswordHash,
            @RoleId,
            @UpdatedAt
        )",
            new
            {
                user.Id,
                user.Username,
                user.Email,
                user.PasswordHash,
                user.RoleId,
                user.UpdatedAt
            });
    }
    
    public async Task UpdateSecurityAsync(User user)
    {
        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(
            "CALL sp_update_user_security(@Id, @FailedLoginAttempts, @LockedUntil)",
            user);
    }

    public async Task DeleteAsync(Guid userId)
    {
        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(
            @"CALL sp_delete_user(@Id)",
            new { Id = userId });
    }
}