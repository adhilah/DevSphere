using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;
using DevSphere.Application.Interfaces.Infrastructure;

namespace DevSphere.Infrastructure.Data;

public class DapperContext : IDapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(
            _configuration.GetConnectionString("DefaultConnection"));
    }
}