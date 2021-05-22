using Models;
using System.Collections.Concurrent;
using System.Threading.Channels;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Commands
{
    public sealed class ConsumeCommand : AsyncCommand
    {

        readonly IShell _consummer;
        readonly IShell _queueShell;
        readonly ConcurrentQueue<Shape> _shareQueue;
        public ConsumeCommand(IShell consummer, IShell queueShell)
        {
            _consummer = consummer;
            _queueShell = queueShell;
            _shareQueue = SingleQueue.ShareQueueLazy;
        }


        public override bool CanExecute(object parameter)
        {           
            return !_queueShell.StatusExecutable && int.TryParse(parameter as string, out int t) && _consummer.StatusExecutable;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _consummer.IsExecuting = true;
                Task t2 = Task.Run(async () => await ReadChannelWhile(parameter));              
                await Task.WhenAll(t2);
            }
            finally
            {
                _consummer.StatusExecutable = true;
                _consummer.StatusText = "finished";
                _consummer.IsExecuting = false;
            }
        }

        public async Task ReadChannelWhile(object parameter)
        {
            Shape shape;
            _consummer.StatusExecutable = false;
            var cancellationToken = new CancellationToken();
            while (await  SingleChannel.ShareChannelReader.WaitToReadAsync(cancellationToken) && !_queueShell.StatusExecutable && _consummer.IsExecuting)
            {
                if (SingleChannel.ShareChannelReader.TryRead(out shape))
                    {
                    _consummer.StatusText = $"Task { Thread.CurrentThread.ManagedThreadId } treated {shape?.Name} {shape?.Id} ";
                    _consummer.FontText = shape.ToString() ;
                }
                if (int.TryParse(parameter as string, out int t))
                    Thread.Sleep(t);
                //await Task.Delay(t);
                else
                    Thread.Sleep(1);
                //await Task.Delay(1);
            }

        }


    }
}
