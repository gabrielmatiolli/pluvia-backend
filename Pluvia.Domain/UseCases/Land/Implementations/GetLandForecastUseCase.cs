using Pluvia.Domain.UseCases.Forecast.Inputs;
using Pluvia.Domain.UseCases.Forecast.Interfaces;
using Pluvia.Domain.UseCases.Forecast.Outputs;
using Pluvia.Domain.UseCases.Land.Inputs;
using Pluvia.Domain.UseCases.Land.Interfaces;

namespace Pluvia.Domain.UseCases.Land.Implementations;

public sealed class GetLandForecastUseCase(ICaptureCurrentForecastUseCase forecastUseCase) : IGetLandForecastUseCase
{
    public Task<CurrentForecastOutput> ExecuteAsync(GetLandForecastInput input, CancellationToken ct)
    {
        return forecastUseCase.ExecuteAsync(
            new CaptureCurrentForecastInput(input.UserId, input.LandId),
            ct);
    }
}
