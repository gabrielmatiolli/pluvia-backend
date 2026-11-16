using Pluvia.Domain.Common;
using Pluvia.Domain.UseCases.Irrigation.Inputs;
using Pluvia.Domain.UseCases.Irrigation.Outputs;

namespace Pluvia.Domain.UseCases.Irrigation.Interfaces;

public interface IApplyIrrigationRiskUseCase : IUseCase<ApplyIrrigationRiskInput, ApplyIrrigationRiskOutput>;
