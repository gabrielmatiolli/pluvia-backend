using Microsoft.EntityFrameworkCore;
using Pluvia.Domain.Entities;
using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Repository.Context;

namespace Pluvia.Repository.Repositories;

public sealed class IrrigationHistoryRepository(PluviaDbContext dbContext) : IIrrigationHistoryRepository
{
    public async Task<string?> GetLatestStatusAsync(ulong landId, CancellationToken ct)
    {
        return await dbContext.IrrigationHistories
            .AsNoTracking()
            .Where(history => history.LandId == landId)
            .OrderByDescending(history => history.Date)
            .Select(history => history.Status)
            .FirstOrDefaultAsync(ct);
    }

    public async Task AddAsync(IrrigationHistory history, CancellationToken ct)
    {
        dbContext.IrrigationHistories.Add(history);
        await dbContext.SaveChangesAsync(ct);
    }
}
