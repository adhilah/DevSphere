using Dapper;
using DevSphere.Application.Interfaces.Infrastructure;
using DevSphere.Application.Interfaces.Repositories;
using DevSphere.Domain.Entities;

namespace DevSphere.Infrastructure.Repositories;

public sealed class TechnologyRepository
    : ITechnologyRepository
{
    private readonly IDapperContext _context;

    public TechnologyRepository(
        IDapperContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(
        string name,
        CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT EXISTS
(
    SELECT 1
    FROM technologies
    WHERE LOWER(name)=LOWER(@Name)
    AND deleted_at IS NULL
);";

        using var connection =
            _context.CreateConnection();

        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(
                sql,
                new
                {
                    Name = name
                },
                cancellationToken: cancellationToken));
    }

    public async Task<int> CreateAsync(
        Technology technology,
        CancellationToken cancellationToken)
    {
        const string sql = @"

INSERT INTO technologies
(
    category_id,
    name,
    slug,
    description,
    image_url,
    position,
    created_by,
    created_at,
    updated_at
)
VALUES
(
    @CategoryId,
    @Name,
    @Slug,
    @Description,
    @ImageUrl,
    @Position,
    @CreatedBy,
    @CreatedAt,
    @UpdatedAt
)
RETURNING id;
";

        using var connection =
            _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(
                sql,
                technology,
                cancellationToken: cancellationToken));
    }

    public async Task<Technology?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        const string sql = @"

SELECT
    id,
    category_id      AS CategoryId,
    name,
    slug,
    description,
    image_url        AS ImageUrl,
    position,
    created_by       AS CreatedBy,
    created_at       AS CreatedAt,
    updated_at       AS UpdatedAt,
    updated_by       AS UpdatedBy,
    deleted_at       AS DeletedAt,
    deleted_by       AS DeletedBy
FROM technologies
WHERE id=@Id
AND deleted_at IS NULL;
";

        using var connection =
            _context.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<Technology>(
            new CommandDefinition(
                sql,
                new
                {
                    Id = id
                },
                cancellationToken: cancellationToken));
    }
}