using Moq;
using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Domain.Tests.Infrastructure;
using Pluvia.Domain.UseCases.Land.Implementations;
using Pluvia.Domain.UseCases.Land.Inputs;
using Xunit;

namespace Pluvia.Domain.Tests.UseCases.Land;

public sealed class DeleteLandUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WhenLandNotFound_ThrowsKeyNotFound()
    {
        var repo = new Mock<ILandRepository>();
        repo.Setup(r => r.GetByIdAsync(1, 1, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Pluvia.Domain.Entities.Land?)null);

        var useCase = new DeleteLandUseCase(repo.Object);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => useCase.ExecuteAsync(new DeleteLandInput(1, 1), CancellationToken.None));
    }

    [Fact]
    public async Task ExecuteAsync_WhenValid_DeletesLand()
    {
        var repo = new Mock<ILandRepository>();
        repo.Setup(r => r.GetByIdAsync(1, 1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(TestDataFactory.CreateLand());
        repo.Setup(r => r.DeleteAsync(It.IsAny<Pluvia.Domain.Entities.Land>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var useCase = new DeleteLandUseCase(repo.Object);

        await useCase.ExecuteAsync(new DeleteLandInput(1, 1), CancellationToken.None);

        repo.Verify(r => r.DeleteAsync(It.IsAny<Pluvia.Domain.Entities.Land>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
