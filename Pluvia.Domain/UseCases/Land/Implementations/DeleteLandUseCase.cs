using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Domain.UseCases.Land.Inputs;
using Pluvia.Domain.UseCases.Land.Interfaces;
using Pluvia.Domain.UseCases.Land.Outputs;

namespace Pluvia.Domain.UseCases.Land.Implementations;

public sealed class DeleteLandUseCase(ILandRepository landRepository) : IDeleteLandUseCase
{
    public async Task<DeleteLandOutput> ExecuteAsync(DeleteLandInput input, CancellationToken ct)
    {
        var land = await landRepository.GetByIdAsync(input.UserId, input.LandId, ct);
        if (land is null)
        {
            throw new KeyNotFoundException();
        }

        await landRepository.DeleteAsync(land, ct);

        return new DeleteLandOutput();
    }
}
