using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.IServices
{
    public interface IOpenAIService
    {
        Task<string> GetAIResponseAsync(string userInput, bool useDatabase = false, List<object> conversationHistory = null);
    }
}
