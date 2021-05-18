using Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Commands
{
    public sealed class ProduceCommand : AsyncCommand
    {
        readonly IShell _shell;
        readonly IShell _consummer;
        readonly ConcurrentQueue<Shape> _shareQueue;
        public ProduceCommand(IShell shell, IShell consummer)
        {
            _shell = shell;
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
                Task t1 = Task.Run(() => FillQueueWhile());
                Task t2 = Task.Run(() => PeakQueueWhile());
                await Task.WhenAll(t1, t2);
            }
            finally
            {
                _shell.StatusText = "finished";
            }
        }

       


        public async Task FillQueueWhile()
        {
            Random rnd = new Random();

            while (_shareQueue.Count < 100)
            {
                var shapeType = rnd.Next(0, 2);
                var shapeFactory  = GetShapeFactory(shapeType);
                var shape = shapeFactory.CreateShape();
                _shell.StatusText = $"Task { Thread.CurrentThread.ManagedThreadId } created {shape.Name} {shape.Id} on queue length {_shareQueue.Count}";
                _shareQueue.Enqueue(shape);
                await Task.Delay(10);
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
