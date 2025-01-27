using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("hello")]
    public async Task<IActionResult> GetHello()
    {
        return Ok("Hello from Controller!");
    }

    [HttpPost("data")]
    public async Task<IActionResult> PostData([FromBody] MyData data)
    {
        return Ok(data);
    }
}