namespace Pluvia.Domain.Repositories.Interfaces;

public interface IIrrigationHistoryRepository
{
    Task<string?> GetLatestStatusAsync(ulong landId, CancellationToken ct);
    Task AddAsync(Pluvia.Domain.Entities.IrrigationHistory history, CancellationToken ct);
}
