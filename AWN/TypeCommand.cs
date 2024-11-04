using System.Threading.Tasks;
using Microsoft.Playwright;

namespace AWN
{
    public class TypeCommand : ICommand
    {
        private readonly IPage _page;

        public TypeCommand(IPage page)
        {
            _page = page;
        }

        public bool CanExecute(string commandInput)
        {
            return commandInput.StartsWith("type ");
        }

        public async Task<string> ExecuteAsync(string commandInput)
        {
            var textToType = commandInput.Substring("type ".Length).Trim();
            await _page.Keyboard.TypeAsync(textToType);
            return "Text typed successfully.";
        }
    }
}
