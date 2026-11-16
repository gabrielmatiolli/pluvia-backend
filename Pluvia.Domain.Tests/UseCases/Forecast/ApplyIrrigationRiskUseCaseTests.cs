using Moq;
using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Domain.Tests.Infrastructure;
using Pluvia.Domain.UseCases.Irrigation.Implementations;
using Pluvia.Domain.UseCases.Irrigation.Inputs;
using Xunit;

namespace Pluvia.Domain.Tests.UseCases.Forecast;

public sealed class ApplyIrrigationRiskUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WhenStatusSame_DoesNotCreateHistory()
    {
        var repo = new Mock<IIrrigationHistoryRepository>();
        repo.Setup(r => r.GetLatestStatusAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync("ativado");

        var useCase = new ApplyIrrigationRiskUseCase(repo.Object);
        var land = TestDataFactory.CreateLand();

        await useCase.ExecuteAsync(new ApplyIrrigationRiskInput(land, DateTime.UtcNow, 90m), CancellationToken.None);

        repo.Verify(r => r.AddAsync(It.IsAny<Pluvia.Domain.Entities.IrrigationHistory>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_WhenStatusChanges_CreatesHistory()
    {
        var repo = new Mock<IIrrigationHistoryRepository>();
        repo.Setup(r => r.GetLatestStatusAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync("desativado");
        repo.Setup(r => r.AddAsync(It.IsAny<Pluvia.Domain.Entities.IrrigationHistory>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var useCase = new ApplyIrrigationRiskUseCase(repo.Object);
        var land = TestDataFactory.CreateLand();

        await useCase.ExecuteAsync(new ApplyIrrigationRiskInput(land, DateTime.UtcNow, 90m), CancellationToken.None);

        repo.Verify(r => r.AddAsync(It.IsAny<Pluvia.Domain.Entities.IrrigationHistory>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
