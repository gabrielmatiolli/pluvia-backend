using Microsoft.EntityFrameworkCore;
using Pluvia.Domain.Entities;
using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Repository.Context;

namespace Pluvia.Repository.Repositories;

public sealed class ForecastHistoryRepository(PluviaDbContext dbContext) : IForecastHistoryRepository
{
    public Task<ForecastHistory?> GetLatestAsync(ulong landId, CancellationToken ct)
    {
        return dbContext.ForecastHistories
            .AsNoTracking()
            .Where(history => history.LandId == landId)
            .OrderByDescending(history => history.Date)
            .FirstOrDefaultAsync(ct);
    }

    public async Task AddAsync(ForecastHistory history, CancellationToken ct)
    {
        dbContext.ForecastHistories.Add(history);
        await dbContext.SaveChangesAsync(ct);
    }
}
