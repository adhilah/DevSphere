using DevSphere.Domain.Entities;

namespace DevSphere.Application.Interfaces.Repositories;

public interface ITechnologyRepository
{
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken);

    Task<int> CreateAsync(Technology technology, CancellationToken cancellationToken);

    Task<Technology?> GetByIdAsync(int id, CancellationToken cancellationToken);
}