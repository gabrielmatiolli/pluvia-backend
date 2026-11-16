using Pluvia.Domain.Common;
using Pluvia.Domain.UseCases.Auth.Inputs;
using Pluvia.Domain.UseCases.Auth.Outputs;

namespace Pluvia.Domain.UseCases.Auth.Interfaces;

public interface IRegisterUserUseCase : IUseCase<RegisterUserInput, RegisterUserOutput>;
