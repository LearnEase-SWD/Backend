﻿using LearnEase.Core.Base;
using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Core.Models.Request;
using LearnEase.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class VideoLessonController : ControllerBase
	{
		private readonly IVideoLessonService _videoLessonService;

		public VideoLessonController(IVideoLessonService videoLessonService)
		{
			_videoLessonService = videoLessonService;
		}

		[HttpGet]
		public async Task<IActionResult> GetVideoLessons(int pageIndex = 1, int pageSize = 10)
		{
			var response = await _videoLessonService.GetVideoLessonsAsync(pageIndex, pageSize);
			return StatusCode((int)response.StatusCode, response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetVideoLessonById(Guid id)
		{
			var response = await _videoLessonService.GetVideoLessonByIdAsync(id);
			return StatusCode((int)response.StatusCode, response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateVideoLesson([FromBody] VideoLessonCreateRequest request)
		{
			var response = await _videoLessonService.CreateVideoLessonAsync(request);
			return StatusCode((int)response.StatusCode, response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateVideoLesson(Guid id, [FromBody] VideoLessonCreateRequest request)
		{
			var response = await _videoLessonService.UpdateVideoLessonAsync(id, request);
			return StatusCode((int)response.StatusCode, response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteVideoLesson(Guid id)
		{
			var response = await _videoLessonService.DeleteVideoLessonAsync(id);
			return StatusCode((int)response.StatusCode, response);
		}
        [HttpPost("mark-completed")]
        public async Task<IActionResult> MarkVideoCompleted(string userId, Guid videoLessonId)
        {
            var response = await _videoLessonService.MarkVideoLessonAsCompletedAsync(userId, videoLessonId);

            if (response.StatusCode == StatusCodeHelper.OK && response.Code == "SUCCESS") // Kiểm tra StatusCode và Code
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
