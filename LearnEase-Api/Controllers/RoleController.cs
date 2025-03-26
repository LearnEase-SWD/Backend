/*using Microsoft.AspNetCore.Mvc;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.Dtos.request;
using Microsoft.AspNetCore.Authorization;

namespace LearnEase_Api.Controllers
{
    [Route("api/role")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequest request)
        {
            if (request == null)
            {
                return BadRequest("Role request cannot be null");
            }

            var result = await _roleService.CreateRole(request);
            if (result == null)
            {
                return NotFound("Role creation failed");
            }

            return Ok(result); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromBody] RoleRequest request)
        {
            if (request == null)
            {
                return BadRequest("Role request cannot be null");
            }

            var result = await _roleService.DeleteRole(request);
            if (result == null)
            {
                return NotFound($"Role with name {request.name} not found");
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("Role name cannot be null or empty");
            }

            var result = await _roleService.GetByName(roleName);
            if (result == null)
            {
                return NotFound($"Role with name '{roleName}' not found");
            }

            return Ok(result);
        }
    }
}
*/