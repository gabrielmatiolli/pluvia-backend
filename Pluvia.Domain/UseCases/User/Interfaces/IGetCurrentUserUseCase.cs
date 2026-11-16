using Pluvia.Domain.Common;
using Pluvia.Domain.UseCases.User.Inputs;
using Pluvia.Domain.UseCases.User.Outputs;

namespace Pluvia.Domain.UseCases.User.Interfaces;

public interface IGetCurrentUserUseCase : IUseCase<GetCurrentUserInput, GetCurrentUserOutput>;
