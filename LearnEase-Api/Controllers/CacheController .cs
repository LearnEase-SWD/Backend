using LearnEase_Api.Dtos.request;
using LearnEase_Api.LearnEase.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase_Api.Controllers
{
    [Route("api/redis")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly IRedisCacheService _redisCacheService;
        public CacheController(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetCache(string key)
        {
            var value = await _redisCacheService.GetAsync<string>(key);
            if (value == null) return NotFound("Key không tồn tại");
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> SetCache([FromBody] CacheRequest request)
        {
            await _redisCacheService.SetAsync(request.key, request.value, TimeSpan.FromMinutes(request.time));
            return Ok("Đã lưu vào cache");
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> RemoveCache(string key)
        {
            await _redisCacheService.RemoveAsync(key);
            return Ok("Đã xóa cache");
        }
    }
}
