using Pluvia.Domain.Common;
using Pluvia.Domain.UseCases.Land.Inputs;
using Pluvia.Domain.UseCases.Forecast.Outputs;

namespace Pluvia.Domain.UseCases.Land.Interfaces;

public interface IGetLandForecastUseCase : IUseCase<GetLandForecastInput, CurrentForecastOutput>;
