using Models;
using System.Collections.Concurrent;
using System.Threading.Channels;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Commands
{
    public sealed class ProduceCommand : AsyncCommand
    {
        readonly IShell _producerShell;
        readonly IShell _queueShell;
        readonly ConcurrentQueue<Shape> _shareQueue;
        public ProduceCommand(IShell producerShell, IShell queueShell)
        {
            _producerShell = producerShell;
            _queueShell = queueShell;
            _shareQueue = SingleQueue.ShareQueueLazy;
        }


        public override bool CanExecute(object parameter)
        {
            
            return !_queueShell.StatusExecutable && int.TryParse(parameter as string ,out int t)
               && _producerShell.StatusExecutable ;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _producerShell.IsExecuting = true;
                await Task.Run(async () => await FillChannelWhile(parameter));
            }
            finally
            {
                _producerShell.StatusText = "finished";
                _producerShell.StatusExecutable = true;
                _producerShell.IsExecuting = false;
            }
        }

        public async Task FillChannelWhile(object parameter)
        {
            _producerShell.StatusExecutable = false;
            Random rnd = new Random();
            var cancellationToken = new CancellationToken();
            while ((await SingleChannel.ShareChannelWriter.WaitToWriteAsync(cancellationToken)) 
                && !_queueShell.StatusExecutable && _producerShell.IsExecuting )
            {
                var shapeType = rnd.Next(0, 3);
                var shapeFactory = GetShapeFactory(shapeType);
                var shape = shapeFactory.CreateShape();
                
                await SingleChannel.ShareChannelWriter.WriteAsync(shape);
                _producerShell.StatusText = $"Task { Thread.CurrentThread.ManagedThreadId } write {shape.Name} {shape.Id} :";
                _producerShell.FontText = shape.ToString();
                if (int.TryParse(parameter as string, out int t))
                    Thread.Sleep(t);
                else
                    Thread.Sleep(1);
            }           
        }


        ShapeFactory GetShapeFactory(int type)
        {
            ShapeFactory shapeFactory = null;
            switch (type)
            {
                case 0:
                    shapeFactory = new CircleFactory();
                    break;
                case 1:
                    shapeFactory = new RegtangleFactory();
                    break;
                case 2:
                    shapeFactory = new TriangleFactory();
                    break;
                default:
                    break;

            }
            return shapeFactory;
        }

    }
}
