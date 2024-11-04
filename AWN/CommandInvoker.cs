using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWN
{
    public class CommandInvoker
    {
        private readonly IEnumerable<ICommand> _commands;

        public CommandInvoker(IEnumerable<ICommand> commands)
        {
            _commands = commands;
        }

        public async Task<string> ExecuteCommandAsync(string commandInput)
        {
            foreach (var command in _commands)
            {
                if (command.CanExecute(commandInput))
                {
                    return await command.ExecuteAsync(commandInput);
                }
            }

            return "No command executed.";
        }
    }
}
