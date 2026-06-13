using DecideAI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DecideAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CopilotController : ControllerBase
{
    private readonly ICopilotService _copilotService;

    public CopilotController(ICopilotService copilotService)
    {
        _copilotService = copilotService;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> AskQuestion([FromBody] AskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Question))
        {
            return BadRequest("Question cannot be empty.");
        }

        var response = await _copilotService.AskQuestionAsync(request.Question);
        
        return Ok(new { Answer = response });
    }
}

public class AskRequest
{
    public string Question { get; set; } = string.Empty;
}
