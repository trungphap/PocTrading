using Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Commands
{
    public sealed class StopTask: AsyncCommand
    {
        
        readonly IShell _stopShell;
       
        public StopTask(IShell stopShell)
        {
            _stopShell = stopShell;
            
        }




        public override bool CanExecute(object parameter)
        {
            return !_stopShell.StatusExecutable;
        }



        public async  override Task ExecuteAsync(object parameter)
        {
            _stopShell.IsExecuting =false;
            _stopShell.FontText ="";          
            await Task.Delay(1);
        }
    
    }
}
