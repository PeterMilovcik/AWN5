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
            var pageTextContent = await _page.ContentAsync();
            var prompt = $"Generate a clear, concise response based on the following prompt:\n\n{commandInput}\n\nWeb Page Content:\n\n{pageTextContent}";
            return await _openAiService.GenerateResponseAsync(prompt);
        }
    }
}
