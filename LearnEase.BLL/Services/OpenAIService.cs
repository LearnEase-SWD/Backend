﻿using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LearnEase.Repository;
using LearnEase.Repository.Repositories;
using LearnEase.Service.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LearnEase.Service.Services
    {
        public class OpenAIService : IOpenAIService
        {
            private readonly ApplicationDbContext _context;
            private readonly HttpClient _httpClient;
            private readonly string _apiUrl;
            private readonly string _apiKey;
            private readonly ILogger<OpenAIService> _logger;

            public OpenAIService(ApplicationDbContext context, HttpClient httpClient, IConfiguration configuration, ILogger<OpenAIService> logger)
            {
                _context = context;
                _httpClient = httpClient;
                _apiUrl = "https://api.openai.com/v1/chat/completions";
                _apiKey = configuration["OpenAI:ApiKey"];
                _logger = logger;
            }

        public async Task<string> GetAIResponseAsync(string userInput, bool useDatabase = false)
        {
            _logger.LogInformation($"Received input: {userInput}, useDatabase: {useDatabase}");

            // Nếu user hỏi về khóa học -> Truy vấn database
            if (userInput.ToLower().Contains("mua khóa học") || userInput.ToLower().Contains("khóa học"))
            {
                // Nếu không đủ thông tin, yêu cầu AI đặt câu hỏi
                if (!useDatabase)
                {
                    return await AskForCourseTopic(userInput);
                }

                // Nếu đã có thông tin, tìm khóa học trong database
                string courseSuggestions = await GenerateCourseSuggestionsFromDatabase(userInput);
                if (!string.IsNullOrEmpty(courseSuggestions))
                {
                    _logger.LogInformation("Returning course suggestions from database.");
                    return courseSuggestions;
                }

                // Nếu không có khóa học, gợi ý các chủ đề khác
                return await AskForCourseTopic(userInput);
            }

            // Nếu user hỏi về ngữ pháp -> Không gọi DB, chỉ gọi AI
            else if (userInput.ToLower().Contains("sửa lỗi") || userInput.ToLower().Contains("ngữ pháp"))
            {
                return await GenerateGrammarCorrection(userInput);
            }

            // Câu hỏi thông thường -> Không gọi DB
            else
            {
                return await GenerateGeneralResponse(userInput);
            }
        }

        private async Task<string> GenerateCourseSuggestionsFromDatabase(string userPreference)
        {
            try
            {
                _logger.LogInformation($"Searching courses for: {userPreference}");

                var courses = await _context.Courses
                    .Where(c => c.Title.ToLower().Contains(userPreference.ToLower()))
                    .ToListAsync();

                if (!courses.Any())
                {
                    _logger.LogInformation("No courses found in database.");
                    return null;
                }

                var courseInfo = new StringBuilder();
                courseInfo.AppendLine("Dựa trên thông tin bạn cung cấp, chúng tôi gợi ý các khóa học sau:");
                foreach (var course in courses)
                {
                    courseInfo.AppendLine($"- {course.Title}, Giá: {course.Price}, Độ khó: {course.DifficultyLevel}");
                }

                return courseInfo.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi truy vấn database: {ex.Message}");
                return "Xin lỗi, hệ thống đang gặp sự cố khi lấy dữ liệu khóa học. Vui lòng thử lại sau!";
            }
        }


        private async Task<string> AskForCourseTopic(string userInput)
        {
            string prompt = $"Người dùng muốn mua khóa học nhưng chưa nói rõ chủ đề. Hãy hỏi họ về lĩnh vực hoặc kỹ năng mà họ muốn học, rồi tiếp tục đặt thêm câu hỏi để làm rõ nhu cầu của họ.";
            return await CallOpenAI(prompt);
        }

        private async Task<string> GenerateGrammarCorrection(string userInput)
            {
                string prompt = $"Sửa lỗi ngữ pháp trong đoạn văn sau: {userInput}";
                return await CallOpenAI(prompt);
            }

            private async Task<string> GenerateGeneralResponse(string userInput)
            {
                return await CallOpenAI(userInput);
            }

            private async Task<string> CallOpenAI(string prompt)
            {
                var requestData = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[] { new { role = "user", content = prompt } }
                };

                var json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

                var response = await _httpClient.PostAsync(_apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"OpenAI API Error: {response.StatusCode} - {error}");
                    return $"Lỗi: {response.StatusCode} - {error}";
                }

                var responseString = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(responseString);

                try
                {
                    return responseObject.choices[0].message.content.ToString().Trim();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"JSON Parsing Error: {ex.Message} - Raw Response: {responseString}");
                    return $"Lỗi phân tích JSON: {ex.Message} - Phản hồi thô: {responseString}";
                }
            }
        }
    }


