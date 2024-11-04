using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace AWN
{
    public class ClickCommand : ICommand
    {
        private readonly OpenAiService _openAiService;
        private readonly IPage _page;

        public ClickCommand(OpenAiService openAiService, IPage page)
        {
            _openAiService = openAiService;
            _page = page;
        }

        public bool CanExecute(string commandInput)
        {
            return commandInput.Contains("click", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<string> ExecuteAsync(string commandInput)
        {
            var htmlContent = await _page.ContentAsync();
            var locator = await _openAiService.IdentifyLocatorAsync(htmlContent, commandInput);

            try
            {
                var locatorElement = _page.Locator(locator);
                await locatorElement.ClickAsync(new LocatorClickOptions { Timeout = 5000 });
                return "Element clicked successfully.";
            }
            catch (TimeoutException)
            {
                return "Failed to locate the correct web element to click.";
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }
    }
}
