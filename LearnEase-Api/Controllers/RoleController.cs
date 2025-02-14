using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.Dtos.request;

namespace LearnEase_Api.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequest request)
        {
            if (request == null)
            {
                return BadRequest("Role request cannot be null");
            }

            var result = await _roleService.createRole(request);
            if (result == null)
            {
                return NotFound("Role creation failed");
            }

            return Ok(result); 
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRole([FromBody] RoleRequest request)
        {
            if (request == null)
            {
                return BadRequest("Role request cannot be null");
            }

            var result = await _roleService.deleteRole(request);
            if (result == null)
            {
                return NotFound($"Role with name {request.name} not found");
            }

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleService.getAllRoles();
            if (result == null || result.Count == 0)
            {
                return NotFound("No roles found");
            }

            return Ok(result);
        }
    }
}
