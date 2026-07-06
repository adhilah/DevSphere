using DevSphere.Application.Interfaces.Repositories;
using DevSphere.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DevSphere.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<
            ITechnologyRepository,
            TechnologyRepository>();

        return services;
    }
}