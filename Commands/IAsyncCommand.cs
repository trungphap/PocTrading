using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Commands
{
    public interface IAsyncCommand : ICommand
    {
        bool CanExecute();
        Task ExecuteAsync();
        IEnumerable<Task> RunningTasks { get; }
    }
}
