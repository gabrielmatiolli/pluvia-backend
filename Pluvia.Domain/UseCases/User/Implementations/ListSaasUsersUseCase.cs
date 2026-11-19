using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Domain.UseCases.User.Interfaces;
using Pluvia.Domain.UseCases.User.Outputs;

namespace Pluvia.Domain.UseCases.User.Implementations;

public sealed class ListSaasUsersUseCase(IUserRepository userRepository) : IListSaasUsersUseCase
{
    public async Task<IReadOnlyList<SaasUserOutput>> ExecuteAsync(CancellationToken ct)
    {
        var users = await userRepository.ListUsersWithLandCountAsync(ct);
        return users
            .Select(user => new SaasUserOutput(user.Name, user.LandCount))
            .ToList();
    }
}
