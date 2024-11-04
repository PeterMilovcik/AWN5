using System;
using System.Threading.Tasks;

namespace AWN
{
    public class ExitCommand : ICommand
    {
        public bool CanExecute(string commandInput)
        {
            return commandInput.Equals("exit", StringComparison.OrdinalIgnoreCase);
        }

        public Task<string> ExecuteAsync(string commandInput)
        {
            Environment.Exit(0);
            return Task.FromResult("Exiting application...");
        }
    }
}
