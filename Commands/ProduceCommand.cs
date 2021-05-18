using Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace Commands
{
    public class ProduceCommand : AsyncCommand
    {

        readonly IShell _shell;
        readonly IShell _consummer;
        public ProduceCommand(IShell shell, IShell consummer)
        {
            _shell = shell;
            _consummer = consummer;
            SharedQueue = new ConcurrentQueue<Shape>();
            RegisterShapes();
        }

        void RegisterShapes()
        {
            ShapeFactory.Register(0, Circle.Create);
            ShapeFactory.Register(1, Rectangle.Create);
            ShapeFactory.Register(2, Triangle.Create);
        }

        public override bool CanExecute()
        {
            return true;
            //return RunningTasks.Count() == 0;
        }

        public override async Task ExecuteAsync()
        {
            //var t = new Thread(FillQueue);
            //t.Start();

            //await Task.Run(async ()=> await FillQueueWhile());
            //await Task.Run(async ()=> await PeakQueueWhile());
            try
            {
                Task t1 = Task.Run(() => FillQueueWhile());
                Task t2 = Task.Run(() => PeakQueueWhile());     
                await Task.WhenAll(t1,t2);
            }
            finally
            {
                _shell.StatusText = "finished";
            }
        }
        
        public ConcurrentQueue<Shape>  SharedQueue {get;set;} 
      

        public async Task FillQueueWhile()
        {
            Random rnd = new Random();

            while (SharedQueue.Count < 100000)
            {
                var shapeType = rnd.Next(0, 2);
                var shape = ShapeFactory.Create(shapeType);
                _shell.StatusText = $"Task { Thread.CurrentThread.ManagedThreadId } created {shape.Name} {shape.Id} on queue length {SharedQueue.Count}";
                SharedQueue.Enqueue(shape);
                await Task.Delay(10);
              }      

        }

        public async Task PeakQueueWhile()
        {
            Shape shape;          
            while (true)
            {
                 SharedQueue.TryDequeue(out shape);
                _consummer.StatusText = $"Task { Thread.CurrentThread.ManagedThreadId } treated {shape?.Name} {shape?.Id} on queue length {SharedQueue.Count}";             
                
                await Task.Delay(100);
            }

        }
    }
}
