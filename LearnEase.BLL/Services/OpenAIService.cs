using System;
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

        // Xử lý yêu cầu từ người dùng và xác định cần truy vấn database hay không
        public async Task<string> GetAIResponseAsync(string userInput, bool useDatabase = false)
        {
            _logger.LogInformation($"Received input: {userInput}, useDatabase: {useDatabase}");

            // Nếu yêu cầu liên quan đến khóa học, xử lý riêng
            if (userInput.ToLower().Contains("mua khóa học") || userInput.ToLower().Contains("khóa học"))
            {
                return await HandleCourseRequest(userInput, useDatabase);
            }

            // Kiểm tra lỗi ngữ pháp và trả lời hội thoại
            return await HandleConversationWithGrammarCheck(userInput);
        }

        // Xử lý yêu cầu liên quan đến khóa học, có thể truy vấn database nếu cần
        private async Task<string> HandleCourseRequest(string userInput, bool useDatabase)
        {
            if (!useDatabase)
            {
                return await AskForCourseTopic(userInput);
            }

            string courseSuggestions = await GenerateCourseSuggestionsFromDatabase(userInput);
            if (!string.IsNullOrEmpty(courseSuggestions))
            {
                _logger.LogInformation("Returning course suggestions from database.");
                return courseSuggestions;
            }

            return await AskForCourseTopic(userInput);
        }

        // Kiểm tra lỗi chính tả/ngữ pháp và trả lời hội thoại
        private async Task<string> HandleConversationWithGrammarCheck(string userInput)
        {
            string correctedText = await CheckGrammarAndSuggestFix(userInput);

            if (string.IsNullOrEmpty(correctedText))
            {
                return await GenerateGeneralResponse(userInput);
            }

            return $"{await GenerateGeneralResponse(userInput)}\n\n{correctedText}";
        }

        // Tạo danh sách gợi ý khóa học từ database
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

        // Hỏi thêm thông tin khi không tìm thấy khóa học phù hợp
        private async Task<string> AskForCourseTopic(string userInput)
        {
            string prompt = "Người dùng muốn mua khóa học nhưng chưa nói rõ chủ đề. Hãy hỏi họ về lĩnh vực hoặc kỹ năng mà họ muốn học, rồi tiếp tục đặt thêm câu hỏi để làm rõ nhu cầu của họ.";
            return await CallOpenAI(prompt);
        }

        // Kiểm tra lỗi ngữ pháp và đề xuất sửa lỗi
        private async Task<string> CheckGrammarAndSuggestFix(string userInput)
        {
            string prompt = $@"
Bạn là một giáo viên tiếng Anh thân thiện. Nếu có lỗi chính tả hoặc ngữ pháp trong câu sau, hãy chỉ ra lỗi và đề xuất sửa lỗi.
Hãy phản hồi như một cuộc trò chuyện tự nhiên, ví dụ:
- 'Có phải bạn muốn nói ""today"" thay vì ""tuday"" không? 😊'
- 'Mình thấy một lỗi nhỏ: ""He go"" → Có phải bạn muốn nói ""He goes"" không?'

Nếu không có lỗi, chỉ cần trả lời tự nhiên mà không nhắc lỗi.

Câu của người dùng: {JsonConvert.SerializeObject(userInput)}";

            string aiResponse = await CallOpenAI(prompt);

            if (aiResponse.ToLower().Contains("không có lỗi") || aiResponse.ToLower().Contains("câu này đúng rồi"))
            {
                return null;
            }

            return aiResponse;
        }

        // Trả lời hội thoại thông thường
        private async Task<string> GenerateGeneralResponse(string userInput)
        {
            return await CallOpenAI(userInput);
        }

        // Gửi yêu cầu đến API OpenAI
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
