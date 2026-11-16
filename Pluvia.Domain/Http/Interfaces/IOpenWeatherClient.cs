using Pluvia.Domain.Http.Models;

namespace Pluvia.Domain.Http.Interfaces;

public interface IOpenWeatherClient
{
    Task<OpenWeatherCurrentWeather> GetCurrentWeatherAsync(decimal latitude, decimal longitude, CancellationToken ct);
}
