using Moq;
using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Domain.Tests.Infrastructure;
using Pluvia.Domain.UseCases.Auth.Implementations;
using Pluvia.Domain.UseCases.Auth.Inputs;
using Xunit;

namespace Pluvia.Domain.Tests.UseCases.Auth;

public sealed class LoginUserUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WhenMissingFields_ThrowsInvalidOperation()
    {
        var repo = new Mock<IUserRepository>();
        var useCase = new LoginUserUseCase(repo.Object, new AuthTokenSettings { Key = new string('x', 32) });

        var input = new LoginUserInput("", "");

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(
            () => useCase.ExecuteAsync(input, CancellationToken.None));

        Assert.Equal("Email e senha sao obrigatorios.", ex.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserNotFound_ThrowsUnauthorized()
    {
        var repo = new Mock<IUserRepository>();
        repo.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Pluvia.Domain.Entities.User?)null);

        var useCase = new LoginUserUseCase(repo.Object, new AuthTokenSettings { Key = new string('x', 32) });

        var input = new LoginUserInput("joao@email.com", "123456");

        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => useCase.ExecuteAsync(input, CancellationToken.None));
    }

    [Fact]
    public async Task ExecuteAsync_WhenPasswordInvalid_ThrowsUnauthorized()
    {
        var repo = new Mock<IUserRepository>();
        repo.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(TestDataFactory.CreateUser(password: "123456"));

        var useCase = new LoginUserUseCase(repo.Object, new AuthTokenSettings { Key = new string('x', 32) });

        var input = new LoginUserInput("joao@email.com", "wrong");

        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => useCase.ExecuteAsync(input, CancellationToken.None));
    }

    [Fact]
    public async Task ExecuteAsync_WhenValid_ReturnsToken()
    {
        var repo = new Mock<IUserRepository>();
        repo.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(TestDataFactory.CreateUser(password: "123456"));

        var useCase = new LoginUserUseCase(repo.Object, new AuthTokenSettings
        {
            Key = new string('x', 32),
            Issuer = "pluvia-api",
            Audience = "pluvia-client",
            ExpiresInMinutes = 60
        });

        var input = new LoginUserInput("joao@email.com", "123456");

        var output = await useCase.ExecuteAsync(input, CancellationToken.None);

        Assert.False(string.IsNullOrWhiteSpace(output.Token));
        Assert.Equal("Joao", output.User.Name);
    }
}
