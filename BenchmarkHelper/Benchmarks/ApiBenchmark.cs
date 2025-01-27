using BenchmarkDotNet.Attributes;

using System.Text;
using System.Text.Json;

public record MyData(string Name, int Age);

[MemoryDiagnoser]
public class ApiBenchmark
{
    [Params(10, 20)]
    public int IterationCount;

    private readonly HttpClient _client = new HttpClient {};

    [Benchmark]
    public async Task MinimalApi_Hello()
    {
        for (int i = 0; i < IterationCount; i++)
        {
            await _client.GetAsync("https://localhost:7000/minimalapi/hello");
        }
    }

    [Benchmark]
    public async Task Controller_Hello()
    {
        for (int i = 0; i < IterationCount; i++)
        {
            await _client.GetAsync("https://localhost:7000/api/test/hello");
        }
    }

    [Benchmark]
    public async Task MinimalApi_PostData()
    {
        for (int i = 0; i < IterationCount; i++)
        {
            var data = new MyData("John", 30);
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            await _client.PostAsync("https://localhost:7000/minimalapi/data", content);
        }
    }

    [Benchmark]
    public async Task Controller_PostData()
    {
        try
        {
            for (int i = 0; i < IterationCount; i++)
            {
                var data = new MyData("John", 30);

                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync("https://localhost:7000/api/test/data", content);
            }
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            throw;
        }

    }
}