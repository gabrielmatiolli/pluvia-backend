using Microsoft.EntityFrameworkCore;
using Pluvia.Domain.Entities;
using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Repository.Context;

namespace Pluvia.Repository.Repositories;

public sealed class LandRepository(PluviaDbContext dbContext) : ILandRepository
{
    public async Task<IReadOnlyList<Land>> GetAllByUserAsync(ulong userId, CancellationToken ct)
    {
        return await dbContext.Lands
            .Include(land => land.Crop)
            .Include(land => land.ForecastHistories)
            .Include(land => land.IrrigationHistories)
            .Where(land => land.UserId == userId)
            .OrderBy(land => land.Name)
            .ToListAsync(ct);
    }

    public Task<Land?> GetByIdAsync(ulong userId, ulong landId, CancellationToken ct)
    {
        return dbContext.Lands
            .Include(land => land.Crop)
            .Include(land => land.ForecastHistories)
            .Include(land => land.IrrigationHistories)
            .FirstOrDefaultAsync(land => land.Id == landId && land.UserId == userId, ct);
    }

    public async Task AddAsync(Land land, CancellationToken ct)
    {
        dbContext.Lands.Add(land);
        await dbContext.SaveChangesAsync(ct);
    }

    public Task UpdateAsync(Land land, CancellationToken ct)
    {
        dbContext.Lands.Update(land);
        return dbContext.SaveChangesAsync(ct);
    }

    public Task DeleteAsync(Land land, CancellationToken ct)
    {
        dbContext.Lands.Remove(land);
        return dbContext.SaveChangesAsync(ct);
    }
}
