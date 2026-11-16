using Moq;
using Pluvia.Domain.Repositories.Interfaces;
using Pluvia.Domain.Tests.Infrastructure;
using Pluvia.Domain.UseCases.Land.Implementations;
using Pluvia.Domain.UseCases.Land.Inputs;
using Xunit;

namespace Pluvia.Domain.Tests.UseCases.Land;

public sealed class CreateLandUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WhenMissingFields_ThrowsInvalidOperation()
    {
        var landRepo = new Mock<ILandRepository>();
        var cropRepo = new Mock<ICropRepository>();
        var useCase = new CreateLandUseCase(landRepo.Object, cropRepo.Object);

        var input = new CreateLandInput(1, 1, "", 10, "", "", null, null, null, 0, 0);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(
            () => useCase.ExecuteAsync(input, CancellationToken.None));

        Assert.Equal("Nome, estado e cidade sao obrigatorios.", ex.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WhenHectaresInvalid_ThrowsInvalidOperation()
    {
        var landRepo = new Mock<ILandRepository>();
        var cropRepo = new Mock<ICropRepository>();
        var useCase = new CreateLandUseCase(landRepo.Object, cropRepo.Object);

        var input = new CreateLandInput(1, 1, "Fazenda", 0, "SP", "Ribeirao", null, null, null, 0, 0);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(
            () => useCase.ExecuteAsync(input, CancellationToken.None));

        Assert.Equal("Hectares deve ser maior que zero.", ex.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WhenCropInvalid_ThrowsInvalidOperation()
    {
        var landRepo = new Mock<ILandRepository>();
        var cropRepo = new Mock<ICropRepository>();
        cropRepo.Setup(r => r.ExistsByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var useCase = new CreateLandUseCase(landRepo.Object, cropRepo.Object);

        var input = new CreateLandInput(1, 1, "Fazenda", 10, "SP", "Ribeirao", null, null, null, 0, 0);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(
            () => useCase.ExecuteAsync(input, CancellationToken.None));

        Assert.Equal("Tipo de plantacao invalido.", ex.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WhenValid_ReturnsOutput()
    {
        var landRepo = new Mock<ILandRepository>();
        var cropRepo = new Mock<ICropRepository>();
        cropRepo.Setup(r => r.ExistsByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        landRepo.Setup(r => r.AddAsync(It.IsAny<Pluvia.Domain.Entities.Land>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Callback<Pluvia.Domain.Entities.Land, CancellationToken>((land, _) => land.Id = 5);
        landRepo.Setup(r => r.GetByIdAsync(1, 5, It.IsAny<CancellationToken>()))
            .ReturnsAsync(TestDataFactory.CreateLand(id: 5, name: "Fazenda"));

        var useCase = new CreateLandUseCase(landRepo.Object, cropRepo.Object);

        var input = new CreateLandInput(1, 1, "Fazenda", 10, "SP", "Ribeirao", null, null, null, 0, 0);
        var output = await useCase.ExecuteAsync(input, CancellationToken.None);

        Assert.Equal(5ul, output.Land.Id);
        Assert.Equal("Fazenda", output.Land.Name);
    }
}
