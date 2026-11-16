using Pluvia.Domain.Entities;
using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Domain.UseCases.Irrigation.Inputs;
using Pluvia.Domain.UseCases.Irrigation.Interfaces;
using Pluvia.Domain.UseCases.Irrigation.Outputs;

namespace Pluvia.Domain.UseCases.Irrigation.Implementations;

public sealed class ApplyIrrigationRiskUseCase(
    IIrrigationHistoryRepository irrigationHistoryRepository) : IApplyIrrigationRiskUseCase
{
    private const decimal IrrigationActivationRiskThreshold = 80m;
    private const string IrrigationActivatedStatus = "ativado";
    private const string IrrigationDeactivatedStatus = "desativado";

    public async Task<ApplyIrrigationRiskOutput> ExecuteAsync(ApplyIrrigationRiskInput input, CancellationToken ct)
    {
        var desiredStatus = input.FireRiskPercent > IrrigationActivationRiskThreshold
            ? IrrigationActivatedStatus
            : IrrigationDeactivatedStatus;

        var lastStatus = await irrigationHistoryRepository.GetLatestStatusAsync(input.Land.Id, ct);
        if (string.Equals(lastStatus, desiredStatus, StringComparison.OrdinalIgnoreCase))
        {
            return new ApplyIrrigationRiskOutput();
        }

        var irrigationHistory = new IrrigationHistory
        {
            LandId = input.Land.Id,
            Date = input.ReferenceDate,
            Status = desiredStatus,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await irrigationHistoryRepository.AddAsync(irrigationHistory, ct);
        input.Land.IrrigationHistories.Add(irrigationHistory);

        return new ApplyIrrigationRiskOutput();
    }
}
