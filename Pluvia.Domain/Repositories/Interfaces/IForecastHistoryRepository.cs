using Pluvia.Domain.Entities;

namespace Pluvia.Domain.Repositories.Interfaces;

public interface IForecastHistoryRepository
{
    Task<ForecastHistory?> GetLatestAsync(ulong landId, CancellationToken ct);
    Task AddAsync(ForecastHistory history, CancellationToken ct);
}
