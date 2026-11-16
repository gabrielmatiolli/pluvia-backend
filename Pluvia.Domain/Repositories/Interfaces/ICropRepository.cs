using Pluvia.Domain.Entities;

namespace Pluvia.Domain.Repositories.Interfaces;

public interface ICropRepository
{
    Task<IReadOnlyList<Crop>> GetAllAsync(CancellationToken ct);
    Task<bool> ExistsByIdAsync(ulong id, CancellationToken ct);
}
