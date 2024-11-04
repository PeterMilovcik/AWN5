using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenAI;
using OpenAI.Chat;

namespace AWN
{
    public class OpenAiService
    {
        private readonly string _apiKey;
        private readonly ChatClient _chatClient;

        public OpenAiService()
        {
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new InvalidOperationException("API key not found.");
            _chatClient = new ChatClient("gpt-4o-mini", _apiKey);
        }

        public async Task<string> GenerateResponseAsync(string prompt)
        {
            var messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a knowledgeable assistant responding to prompts related to web page content."),
                new UserChatMessage($"Generate a clear, concise response based on the following prompt:\n\n{prompt}")
            };

            try
            {
                ChatCompletion completion = await _chatClient.CompleteChatAsync(messages);
                return completion.Content[0].Text;
            }
            catch (Exception ex)
            {
                // Handle error (log it, rethrow it, etc.)
                return $"Error generating response: {ex.Message}";
            }
        }
    }
}
