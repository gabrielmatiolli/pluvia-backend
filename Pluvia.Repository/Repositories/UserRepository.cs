using Microsoft.EntityFrameworkCore;
using Pluvia.Domain.Entities;
using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Repository.Context;

namespace Pluvia.Repository.Repositories;

public sealed class UserRepository(PluviaDbContext dbContext) : IUserRepository
{
    public Task<User?> GetByIdAsync(ulong id, CancellationToken ct)
    {
        return dbContext.Users
            .FirstOrDefaultAsync(user => user.Id == id, ct);
    }

    public Task<User?> GetByEmailAsync(string normalizedEmail, CancellationToken ct)
    {
        return dbContext.Users
            .FirstOrDefaultAsync(user => user.Email == normalizedEmail, ct);
    }

    public Task<bool> ExistsByEmailAsync(string normalizedEmail, CancellationToken ct)
    {
        return dbContext.Users
            .AsNoTracking()
            .AnyAsync(user => user.Email == normalizedEmail, ct);
    }

    public async Task AddAsync(User user, CancellationToken ct)
    {
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(ct);
    }

    public Task UpdateAsync(User user, CancellationToken ct)
    {
        dbContext.Users.Update(user);
        return dbContext.SaveChangesAsync(ct);
    }
}
