namespace Pluvia.Domain.UseCases.Forecast.Inputs;

public sealed record RefreshLandForecastHistoriesInput(IReadOnlyList<Pluvia.Domain.Entities.Land> Lands);
