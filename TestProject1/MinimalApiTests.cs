using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

public class MinimalApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

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
    public async Task PostUserData_ShouldReturnOk()
    {
        // Arrange
        var requestData = new { Name = "Jane Doe", Age = 25 };

        // Act
        var response = await _client.PostAsJsonAsync("/minimalapi/create", requestData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostUserV1Data_ShouldReturnOk()
    {
        // Arrange
        var requestData = new { Name = "Jane Doe", Age = 25 };

        // Act
        var response = await _client.PostAsJsonAsync("/minimalapi/createV1", requestData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostUserData_ShouldReturnError()
    {
        // Arrange
        var requestData = new { Name = "Jane Doe", Age = 150 };

        // Act
        var response = await _client.PostAsJsonAsync("/minimalapi/create", requestData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        // Deserialize the response content
        var responseContent = await response.Content.ReadAsStringAsync();
        var errorResponse = JsonSerializer.Deserialize<ValidationErrorResponse>(responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Check if errors are present
        errorResponse.Should().NotBeNull();
        errorResponse.Errors.Should().ContainKey("Age");
    }

    [Fact]
    public async Task PostUserV1Data_ShouldReturnError()
    {
        // Arrange
        var requestData = new { Name = "Jane Doe", Age = 150 };

        // Act
        var response = await _client.PostAsJsonAsync("/minimalapi/createV1", requestData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        // Deserialize the response content
        var responseContent = await response.Content.ReadAsStringAsync();
        var errorResponse = JsonSerializer.Deserialize<ValidationErrorResponse>(responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Check if errors are present
        errorResponse.Should().NotBeNull();
        errorResponse.Errors.Should().ContainKey("Age");
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

