using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace AWN
{
    public class NavigateCommand : ICommand
    {
        private readonly IPage _page;

        public NavigateCommand(IPage page)
        {
            _page = page;
        }

        public bool CanExecute(string commandInput)
        {
            return commandInput.StartsWith("navigate to ", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<string> ExecuteAsync(string commandInput)
        {
            var url = commandInput.Substring("navigate to ".Length).Trim();

            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = "https://" + url;
            }

            try
            {
                await _page.GotoAsync(url);
                return "Page loaded successfully.";
            }
            catch (Exception)
            {
                return $"Failed to navigate to {url}";
            }
        }
    }
}
