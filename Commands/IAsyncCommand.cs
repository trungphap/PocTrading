using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Commands
{
    public interface IAsyncCommand : ICommand
    {
        //bool CanExecute(object parameter);
        Task ExecuteAsync(object parameter);
        IEnumerable<Task> RunningTasks { get; }
    }
}
