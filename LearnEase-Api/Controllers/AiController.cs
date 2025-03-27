using System;
using System.Threading.Tasks;
using LearnEase.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearnEase.Api.Controllers
{
    [Route("api/ai")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IOpenAIService _aiService;
        private readonly ILogger<AIController> _logger;

        public AIController(IOpenAIService aiService, ILogger<AIController> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskAI([FromBody] string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return BadRequest("⚠️ Vui lòng nhập câu hỏi!");
            }

            try
            {
                _logger.LogInformation($"AI request received: {userInput}");
                string response = await _aiService.GetAIResponseAsync(userInput, useDatabase: true);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ AI processing error: {ex.Message}");
                return StatusCode(500, "⚠️ Đã xảy ra lỗi hệ thống, vui lòng thử lại sau!");
            }
        }
    }
}
