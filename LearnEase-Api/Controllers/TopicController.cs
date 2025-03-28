﻿using LearnEase.Core.Models.Request;
using LearnEase.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LearnEase_Api.Controllers
{
	[Route("api/topics")]
	[ApiController]
	public class TopicController : ControllerBase
	{
		private readonly ITopicService _topicService;

		public TopicController(ITopicService topicService)
		{
			_topicService = topicService;
		}

		[HttpGet]
		public async Task<IActionResult> GetTopics(int pageIndex = 1, int pageSize = 10)
		{
			var response = await _topicService.GetTopicsAsync(pageIndex, pageSize);
			return StatusCode((int)response.StatusCode, response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetTopicById(Guid id)
		{
			var response = await _topicService.GetTopicByIdAsync(id);
			return StatusCode((int)response.StatusCode, response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateTopic([FromBody] string name)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var response = await _topicService.CreateTopicAsync(name);
			return StatusCode((int)response.StatusCode, response);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTopic(Guid id, [FromBody] string name)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var response = await _topicService.UpdateTopicAsync(id, name);
			return StatusCode((int)response.StatusCode, response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTopic(Guid id)
		{
			var response = await _topicService.DeleteTopicAsync(id);
			return StatusCode((int)response.StatusCode, response);
		}
	}
}
