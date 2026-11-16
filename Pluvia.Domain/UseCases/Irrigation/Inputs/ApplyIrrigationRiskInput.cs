namespace Pluvia.Domain.UseCases.Irrigation.Inputs;

public sealed record ApplyIrrigationRiskInput(
    Pluvia.Domain.Entities.Land Land,
    DateTime ReferenceDate,
    decimal FireRiskPercent);
