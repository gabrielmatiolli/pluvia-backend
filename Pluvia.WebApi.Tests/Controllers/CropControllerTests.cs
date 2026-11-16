using System.Net;
using Pluvia.WebApi.Tests.Infrastructure;
using Xunit;

namespace Pluvia.WebApi.Tests.Controllers;

public sealed class CropControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CropControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetCrops_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/crops");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
