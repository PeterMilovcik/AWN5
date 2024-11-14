using System.Threading.Tasks;
using Microsoft.Playwright;

namespace AWN
{
    public class AiCommand : ICommand
    {
        private readonly OpenAiService _openAiService;
        private readonly IPage _page;

        public AiCommand(OpenAiService openAiService, IPage page)
        {
            _openAiService = openAiService;
            _page = page;
        }

        public bool CanExecute(string commandInput)
        {
            return !string.IsNullOrEmpty(commandInput);
        }

        public async Task<string> ExecuteAsync(string commandInput)
        {
            var pageTextContent = await _page.InnerTextAsync("body");
            var language = Environment.GetEnvironmentVariable("DEFAULT_LANGUAGE") ?? "en";
            var prompt = $"Generate a clear, concise response based on the following prompt:\n\n{commandInput}\n\nWeb Page Content:\n\n{pageTextContent}\n\nLanguage: {language}";
            return await _openAiService.GenerateResponseAsync(prompt);
        }
    }
}
