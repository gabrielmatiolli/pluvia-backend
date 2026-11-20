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
            try
            {
                await captureUseCase.CaptureCurrentForecastAsync(land, ct);
            }
            catch (Exception) when (ct.IsCancellationRequested is false)
            {
                // A previsao do tempo e um dado complementar: se o provedor externo
                // falhar (chave invalida, indisponibilidade, etc.), a listagem de
                // terrenos nao deve quebrar.
            }
        }

        return new RefreshLandForecastHistoriesOutput();
    }
}
