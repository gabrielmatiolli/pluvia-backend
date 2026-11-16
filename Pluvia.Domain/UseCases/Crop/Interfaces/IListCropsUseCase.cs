using Pluvia.Domain.Common;
using Pluvia.Domain.UseCases.Crop.Inputs;
using Pluvia.Domain.UseCases.Crop.Outputs;

namespace Pluvia.Domain.UseCases.Crop.Interfaces;

public interface IListCropsUseCase : IUseCase<ListCropsInput, ListCropsOutput>;
