using Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Commands
{
    public sealed class QueueCommandG<T> : AsyncCommandG<T>
    {
        static bool ExecutedOnce = false;
        readonly IShell _queue;
        readonly ConcurrentQueue<Shape> _shareQueue;
        public QueueCommandG(IShell queue)
        {
            _queue = queue;
            _shareQueue = SingleQueue.ShareQueueLazy;
        }


        public override bool CanExecute(T param)
        {
            return int.TryParse(param as string, out int t) && !ExecutedOnce;
        }

        public override async Task ExecuteAsync(T param)
        {
            await SetChanelLength(param);
        }

        private async Task SetChanelLength(T param)
        {
            _queue.StatusExecutable = true;
            _queue.FontText = "#eee";
            ExecutedOnce = true;
            if (int.TryParse(param as string, out int t))
                SingleChannel.SetChannel(t);
            else
                SingleChannel.SetChannel(1000);
            await Task.Delay(1);
        }
    }
}
