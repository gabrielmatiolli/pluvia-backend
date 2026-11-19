using System.Net;
using System.Net.Http.Json;
using Pluvia.WebApi.Tests.Infrastructure;
using Xunit;

namespace Pluvia.WebApi.Tests.Controllers;

public sealed class UserControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UserControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetMe_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/users/me");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UpdateMe_ReturnsOk()
    {
        var response = await _client.PutAsJsonAsync("/api/users/me", new
        {
            name = "Joao Atualizado",
            email = "joao@email.com"
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetSaasUsers_ReturnsNameAndLandCount()
    {
        _client.DefaultRequestHeaders.Add("X-Test-Role", "Admin");
        var response = await _client.GetAsync("/api/users");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("\"name\":\"Joao\"", body);
        Assert.Contains("\"landCount\":1", body);
        Assert.DoesNotContain("password", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("email", body, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task GetSaasUsers_RejectsRegularUser()
    {
        var response = await _client.GetAsync("/api/users");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}
