using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Domain.UseCases.Crop.Inputs;
using Pluvia.Domain.UseCases.Crop.Interfaces;
using Pluvia.Domain.UseCases.Crop.Outputs;

namespace Pluvia.Domain.UseCases.Crop.Implementations;

public sealed class ListCropsUseCase(ICropRepository cropRepository) : IListCropsUseCase
{
    public async Task<ListCropsOutput> ExecuteAsync(ListCropsInput input, CancellationToken ct)
    {
        var crops = await cropRepository.GetAllAsync(ct);
        var outputs = crops
            .OrderBy(crop => crop.Type)
            .Select(crop => new CropOutput(crop.Id, crop.Type))
            .ToList();

        return new ListCropsOutput(outputs);
    }
}
