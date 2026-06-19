using Dapper;
using DevSphere.Application.Interfaces.Repositories;
using DevSphere.Domain.Entities;
using DevSphere.Infrastructure.Data;

namespace DevSphere.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DapperContext _context;

    public RefreshTokenRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<RefreshToken>(
            "SELECT * FROM fn_get_refresh_token_by_token(@Token)",
            new { Token = token });
    }

    public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(Guid userId)
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryAsync<RefreshToken>(
            "SELECT * FROM fn_get_token_by_user(@UserId)",
            new { UserId = userId });
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(
            @"CALL sp_add_refresh_token(
                @Id,
                @UserId,
                @Token,
                @ExpiresAt,
                @IsRevoked,
                @CreatedAt,
                @UpdatedAt
            )",
            refreshToken);
    }

    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(
            @"CALL sp_update_refresh_token(
                @Id,
                @Token,
                @ExpiresAt,
                @IsRevoked,
                @UpdatedAt
            )",
            refreshToken);
    }

    public async Task RevokeAsync(Guid id)
    {
        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(
            "CALL sp_revoke_refresh_token(@Id)",
            new { Id = id });
    }
}