﻿using Microsoft.AspNetCore.Mvc;
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

            var result = await _roleService.CreateRole(request);
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

            var result = await _roleService.DeleteRole(request);
            if (result == null)
            {
                return NotFound($"Role with name {request.name} not found");
            }

            return Ok(result);
        }

    }
}
