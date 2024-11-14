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

        public async Task<string> IdentifyLocatorAsync(string htmlContent, string elementDescription)
        {
            var prompt = $"Given the HTML content: {htmlContent} and a target element described as {elementDescription}, identify the most precise and unique selector string that can be used with page.Locator in Playwright. Output only the locator string as plain text, ensuring it is not wrapped in any formatting or code blocks.";
            var messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a knowledgeable assistant responding to prompts related to web page content."),
                new UserChatMessage(prompt)
            };

            try
            {
                ChatCompletion completion = await _chatClient.CompleteChatAsync(messages);
                return completion.Content[0].Text.Trim();
            }
            catch (Exception ex)
            {
                // Handle error (log it, rethrow it, etc.)
                return $"Error identifying locator: {ex.Message}";
            }
        }

        public async Task<string> TranslateLanguageToIsoCodeAsync(string language)
        {
            var prompt = $"Translate the following language to its ISO 639-1 locale code: {language}. Output only the ISO 639-1 locale code as base language, and nothing else.";
            var messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a knowledgeable assistant responding to prompts related to language translation."),
                new UserChatMessage(prompt)
            };

            try
            {
                ChatCompletion completion = await _chatClient.CompleteChatAsync(messages);
                return completion.Content[0].Text.Trim();
            }
            catch (Exception ex)
            {
                // Handle error (log it, rethrow it, etc.)
                return $"Error translating language: {ex.Message}";
            }
        }
    }
}
