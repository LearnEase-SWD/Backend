using System.Text;
using System.Text.RegularExpressions;
using LearnEase.Repository;
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

        public async Task<string> GetAIResponseAsync(string userInput, bool useDatabase = false, List<object> conversationHistory = null)
        {
            _logger.LogInformation($"Received input: {userInput}, useDatabase: {useDatabase}");

            string lowerInput = userInput.ToLower();

            if (lowerInput.Contains("khóa học") || lowerInput.Contains("mua khóa học") || lowerInput.Contains("course") || lowerInput.Contains("lập trình"))
            {
                string clarifiedTopic = await AskForCourseTopic(userInput, conversationHistory);

                if (!useDatabase)
                {
                    return clarifiedTopic;
                }

                if (clarifiedTopic.ToLower().Contains("chủ đề cụ thể nào") || clarifiedTopic.Contains("muốn học về chủ đề nào"))
                {
                    return clarifiedTopic;
                }

                string courseSuggestions = await GenerateCourseSuggestionsFromDatabase(clarifiedTopic, conversationHistory);

                return !string.IsNullOrEmpty(courseSuggestions)
                    ? courseSuggestions
                    : $"Hiện tại chúng tôi không có khóa học về '{clarifiedTopic}'. Bạn có thể tham khảo các chủ đề khác bằng cách nhập 'hiện tại có chủ đề nào'.";
            }
            else if (lowerInput.Contains("chủ đề nào") || lowerInput.Contains("ví dụ đi") || lowerInput.Contains("web của bạn có khóa học nào"))
            {
                return await ListAllAvailableCourseTopicsFromDatabase();
            }

            return await HandleConversationWithGrammarCheck(userInput, conversationHistory);
        }
        private async Task<string> ListAllAvailableCourseTopicsFromDatabase()
        {
            try
            {
                var courses = await _context.Courses
                    .Select(c => c.Topic)
                    .Distinct()
                    .ToListAsync();

                if (!courses.Any())
                {
                    return "Hiện tại chúng tôi chưa có khóa học nào trong hệ thống. Bạn có thể quay lại sau!";
                }

                var sb = new StringBuilder();
                sb.AppendLine("Các khóa học đang có trên hệ thống thuộc các chủ đề sau:");
                foreach (var topic in courses)
                {
                    sb.AppendLine($"- {topic}");
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi truy vấn database: {ex.Message}");
                return "Xin lỗi, hệ thống đang gặp sự cố khi truy xuất dữ liệu khóa học.";
            }
        }


        private async Task<string> HandleConversationWithGrammarCheck(string userInput, List<object> conversationHistory = null)
        {
            string correctedText = await CheckGrammarAndSuggestFix(userInput);

            return correctedText ?? await GenerateGeneralResponse(userInput);
        }

        private async Task<string> GenerateCourseSuggestionsFromDatabase(string userPreference, List<object> conversationHistory = null)
        {
            try
            {
                _logger.LogInformation($"Searching courses for: {userPreference}");
                var courses = await _context.Courses
                    .Where(c => c.Title.ToLower().Contains(userPreference.ToLower()))
                    .ToListAsync();

                if (!courses.Any())
                {
                    return $"Hiện tại chúng tôi không có khóa học nào về '{userPreference}'. Bạn có thể nhập 'hiện tại có chủ đề nào' để xem danh sách khóa học có sẵn.";
                }

                var courseInfo = new StringBuilder();
                courseInfo.AppendLine($"Các khóa học về '{userPreference}' mà chúng tôi có là:");
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
        private async Task<string> AskForCourseTopic(string userInput, List<object> conversationHistory = null)
        {
            string pattern = @"(?:tôi muốn mua|mua|tôi cần|tôi muốn học|có khóa học gì về)?\s*(.*)";
            Match match = Regex.Match(userInput, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string extractedTopic = match.Groups[1].Value.Trim();

                if (!string.IsNullOrEmpty(extractedTopic) && extractedTopic.Length > 2)
                {
                    _logger.LogInformation($"Chủ đề khóa học được xác định: {extractedTopic}");
                    return extractedTopic;
                }
            }

            _logger.LogInformation($"Không xác định được chủ đề từ input: {userInput}");
            return "Bạn đang tìm kiếm khóa học về lĩnh vực nào? Ví dụ: lập trình, thiết kế đồ họa, kinh doanh...";
        }


        private async Task<string> CheckGrammarAndSuggestFix(string userInput)
{
                    string prompt = $@"
                Bạn là một giáo viên tiếng Anh thân thiện. 
                Hãy kiểm tra các từ và cụm từ trong câu sau để tìm lỗi chính tả hoặc ngữ pháp.
                Nếu có lỗi, hãy liệt kê các lỗi và đề xuất sửa lỗi, mỗi lỗi trên một dòng.
                Nếu không có lỗi, trả về ""Không có lỗi"".

                Câu của người dùng: {userInput}";

    string response = await CallOpenAI(prompt);

    // Kiểm tra phản hồi từ OpenAI
    if (response.ToLower().Contains("không có lỗi"))
    {
        return null; // Không có lỗi, trả về null
    }

    return response; // Trả về danh sách lỗi và đề xuất sửa lỗi
}


        private async Task<string> GenerateGeneralResponse(string userInput, List<object> conversationHistory = null)
        {
            return await CallOpenAI(userInput, conversationHistory); // Pass conversationHistory here
        }

        private async Task<string> CallOpenAI(string prompt, List<object> conversationHistory = null)
        {
            var messages = new List<object>();
            messages.Add(new { role = "system", content = "Bạn là trợ lý ảo của LearnEase, một ứng dụng học trực tuyến chuyên bán các khóa học với nhiều thể loại/chủ đề và giá cả ưu đãi. Bạn sẽ hỗ trợ người dùng tìm kiếm và mua các khóa học phù hợp." });


            if (conversationHistory != null)
            {
                messages.AddRange(conversationHistory);
            }

            messages.Add(new { role = "user", content = prompt });

            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = messages
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

