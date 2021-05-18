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
        readonly ConcurrentQueue<Shape> _shareQueue;
        public ProduceCommand(IShell shell)
        {
            _shell = shell;        
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
               await Task.Run(async () => await FillQueueWhile());               
            }
            finally
            {
                _shell.StatusText = "finished";
            }
        }

       


        public async Task FillQueueWhile()
        {
            Random rnd = new Random();

            while (_shareQueue.Count < 10000000)
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

    }
}
