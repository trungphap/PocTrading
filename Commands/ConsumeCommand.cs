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
            return _queueShell.StatusExecutable && int.TryParse(parameter as string, out int t);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {               
                Task t2 = Task.Run(async () => await ReadChannelWhile(parameter));
                //Task t2 = Task.Run(async () => await PeakQueueWhile());
                await Task.WhenAll(t2);
            }
            finally
            {
              
            }
        }

        public async Task ReadChannelWhile(object parameter)
        {
            Shape shape;


            while (await  SingleChannel.ShareChannelReader.WaitToReadAsync())
            {
                if (SingleChannel.ShareChannelReader.TryRead(out shape))
                    {
                    _consummer.StatusText = $"Task { Thread.CurrentThread.ManagedThreadId } treated {shape?.Name} {shape?.Id} from channel";
                }
                if (int.TryParse(parameter as string, out int t))
                    await Task.Delay(t);
                else
                    await Task.Delay(1);
            }

        }


        public async Task PeakQueueWhile()
        {
            Shape shape;
            while (true)
            {
                _shareQueue.TryDequeue(out shape);
                _consummer.StatusText = $"Task { Thread.CurrentThread.ManagedThreadId } treated {shape?.Name} {shape?.Id} on queue length {_shareQueue.Count}";

               await Task.Delay(100);
            }

        }
    }
}
