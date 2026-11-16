using Pluvia.Domain.UseCases.Forecast.Inputs;
using Pluvia.Domain.UseCases.Forecast.Interfaces;
using Pluvia.Domain.UseCases.Forecast.Outputs;

namespace Pluvia.Domain.UseCases.Forecast.Implementations;

public sealed class RefreshLandForecastHistoriesUseCase(
    CaptureCurrentForecastUseCase captureUseCase) : IRefreshLandForecastHistoriesUseCase
{
    public async Task<RefreshLandForecastHistoriesOutput> ExecuteAsync(RefreshLandForecastHistoriesInput input, CancellationToken ct)
    {
        foreach (var land in input.Lands)
        {
            await captureUseCase.CaptureCurrentForecastAsync(land, ct);
        }

        return new RefreshLandForecastHistoriesOutput();
    }
}
