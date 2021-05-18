using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Commands
{
    public interface IAsyncCommandG<in T> : ICommand
    {
        bool CanExecute(T parameter);
        Task ExecuteAsync(T parameter);
        IEnumerable<Task> RunningTasks { get; }
    }
}
