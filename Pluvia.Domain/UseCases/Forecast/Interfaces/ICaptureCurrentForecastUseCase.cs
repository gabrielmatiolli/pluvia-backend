using Pluvia.Domain.Common;
using Pluvia.Domain.UseCases.Forecast.Inputs;
using Pluvia.Domain.UseCases.Forecast.Outputs;

namespace Pluvia.Domain.UseCases.Forecast.Interfaces;

public interface ICaptureCurrentForecastUseCase : IUseCase<CaptureCurrentForecastInput, CurrentForecastOutput>;
