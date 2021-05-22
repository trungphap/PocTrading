using Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Commands
{
    public class OpenCommand : AsyncCommand
    {
        readonly IShell _shell;
        public OpenCommand(IShell shell)
        {
            _shell = shell;
        }
        public override bool CanExecute(object parameter)
        {
            return true;          
        }

        public override async Task ExecuteAsync(object parameter)
        {
           
            _shell.StatusText = $"Open Async task {Thread.CurrentThread.ManagedThreadId } of {RunningTasks.Count()} ";

            await Task.Delay(2000);
            _shell.StatusText = $"Completed Open Async task {Thread.CurrentThread.ManagedThreadId } of {RunningTasks.Count()} ";

        }
    }
}
