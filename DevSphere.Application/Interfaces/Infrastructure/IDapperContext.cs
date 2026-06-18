using System.Data;

namespace  DevSphere.Application.Interfaces.Infrastructure;

public interface IDapperContext
{
    IDbConnection CreateConnection();
}

