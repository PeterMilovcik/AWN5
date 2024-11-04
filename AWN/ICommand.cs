using System.Threading.Tasks;

namespace AWN
{
    public interface ICommand
    {
        bool CanExecute(string commandInput);
        Task<string> ExecuteAsync(string commandInput);
    }
}
