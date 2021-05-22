using Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Commands
{
    public sealed class StopCommand: AsyncCommand
    {
        
        readonly IShell _stopShell;
       
        public StopCommand(IShell stopShell)
        {
            _stopShell = stopShell;
            
        }




        public override bool CanExecute(object parameter)
        {
            return !_stopShell.StatusExecutable;
        }



        public async  override Task ExecuteAsync(object parameter)
        {
            _stopShell.StatusExecutable =true;
            _stopShell.FontText ="";
           if(SingleChannel.ShareChannelWriter.TryComplete()) SingleChannel.ResetChannel();
            await Task.Delay(1);
        }
    
    }
}
