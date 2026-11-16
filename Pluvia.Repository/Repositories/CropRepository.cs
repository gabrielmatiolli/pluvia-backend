using Microsoft.EntityFrameworkCore;
using Pluvia.Domain.Entities;
using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Repository.Context;

namespace Pluvia.Repository.Repositories;

public sealed class CropRepository(PluviaDbContext dbContext) : ICropRepository
{
    public async Task<IReadOnlyList<Crop>> GetAllAsync(CancellationToken ct)
    {
        return await dbContext.Crops
            .AsNoTracking()
            .OrderBy(crop => crop.Type)
            .ToListAsync(ct);
    }

    public Task<bool> ExistsByIdAsync(ulong id, CancellationToken ct)
    {
        return dbContext.Crops
            .AsNoTracking()
            .AnyAsync(crop => crop.Id == id, ct);
    }
}
