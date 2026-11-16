namespace Pluvia.Domain.UseCases.Land.Inputs;

public sealed record UpdateLandInput(
    ulong UserId,
    ulong LandId,
    string? Name,
    decimal? Hectares);
