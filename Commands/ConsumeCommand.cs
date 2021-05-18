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
        readonly ConcurrentQueue<Shape> _shareQueue;
        public ConsumeCommand(IShell consummer)
        {
            _consummer = consummer;
            _shareQueue = SingleQueue.ShareQueueLazy;
        }


        public override bool CanExecute()
        {
            return true;
            //return RunningTasks.Count() == 0;
        }

        public override async Task ExecuteAsync()
        {
            try
            {               
                Task t2 = Task.Run(async () => await ReadChannelWhile());
                //Task t2 = Task.Run(async () => await PeakQueueWhile());
                await Task.WhenAll(t2);
            }
            finally
            {
              
            }
        }

        public async Task ReadChannelWhile()
        {
            Shape shape;


            while (await  SingleChannel.ShareChannelReader.WaitToReadAsync())
            {
                if (SingleChannel.ShareChannelReader.TryRead(out shape))
                    {
                    _consummer.StatusText = $"Task { Thread.CurrentThread.ManagedThreadId } treated {shape?.Name} {shape?.Id} from channel";
                }               
               // await Task.Delay(100);
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
