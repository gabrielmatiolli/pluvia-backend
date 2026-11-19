using Pluvia.Domain.UseCases.User.Outputs;

namespace Pluvia.Domain.UseCases.User.Interfaces;

public interface IListSaasUsersUseCase
{
    Task<IReadOnlyList<SaasUserOutput>> ExecuteAsync(CancellationToken ct);
}
