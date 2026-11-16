using Pluvia.Domain.Common;
using Pluvia.Domain.UseCases.Land.Inputs;
using Pluvia.Domain.UseCases.Land.Outputs;

namespace Pluvia.Domain.UseCases.Land.Interfaces;

public interface IDeleteLandUseCase : IUseCase<DeleteLandInput, DeleteLandOutput>;
