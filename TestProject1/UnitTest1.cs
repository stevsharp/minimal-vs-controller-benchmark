using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

public class MinimalApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = new();

    public MinimalApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetHello_ShouldReturnExpectedResponse()
    {
        // Act
        var response = await _client.GetAsync("/minimalapi/hello");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("Hello from Minimal API!");
    }

    [Fact]
    public async Task PostData_ShouldReturnSameData()
    {
        // Arrange
        var requestData = new { Name = "Jane Doe", Age = 25 };

        // Act
        var response = await _client.PostAsJsonAsync("/minimalapi/data", requestData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Controller_GetHello_ShouldReturnExpectedResponse()
    {
        // Act
        var response = await _client.GetAsync("/api/test/hello");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("Hello from Controller!");
    }

    [Fact]
    public async Task Controller_PostData_ShouldReturnSameData()
    {
        // Arrange
        var requestData = new { Name = "John Smith", Age = 35 };

        // Act
        var response = await _client.PostAsJsonAsync("/api/test/data", requestData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

}

