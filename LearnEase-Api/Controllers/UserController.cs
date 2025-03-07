﻿using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Google.Apis.Auth.OAuth2.Requests;
using System.IdentityModel.Tokens.Jwt;
using Google.Apis.Auth;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.Dtos.request;

namespace LearnEase_Api.Controllers
{
    [AllowAnonymous]
    [Route("api/users")]
    [ApiController] 
    public class UserController : ControllerBase    {
        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;

        public UserController(IUserService userService, HttpClient httpClient)
        {
            _userService = userService;
            _httpClient = httpClient;
        }

        //Thầy Không cho lấy hết toàn bộ, muốn lấy phải phân trang
        //(dùng BasePaginatedList trong folder Base để phân trang)

        /*[HttpGet]      
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.getAllUser();
            if (result == null || result.Count == 0)
            {
                return NotFound("No users found.");
            }
            return Ok(result);
        }*/

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserReponseById(id);
            if (result == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNewUser([FromBody] userCreationRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await _userService.CreateNewUser(request);
            if (result == null)
            {
                return BadRequest("Failed to create user. Email might already exist.");
            }
            return CreatedAtAction(nameof(GetUserById), new { id = result.id }, result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request, string id)
        {
            if (request == null)
            {
                return BadRequest("Invalid update data.");
            }

            var result = await _userService.UpdateUserReponse(request, id);
            if (result == null)
            {
                return BadRequest("Failed to update user. Email might already exist.");
            }
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserReponseById(id);
            if (result==null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok("User deleted successfully.");
        }

        [HttpGet("getUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.FindUserByEmail(email);
            if (result == null)
            {
                return NotFound($"User with email {email} not found.");
            }
            return Ok(result);
        }

    }
}
