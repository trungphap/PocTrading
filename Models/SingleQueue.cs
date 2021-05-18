using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public sealed class SingleQueue
    {
        private static readonly Lazy<ConcurrentQueue<Shape>> _shareQueueLazy = new Lazy < ConcurrentQueue < Shape >>  ( ()=>new ConcurrentQueue<Shape>());
        private SingleQueue()
        {

        }
        static SingleQueue()
        {

        }

        public static ConcurrentQueue<Shape> ShareQueueLazy
        {
            get
            {
                return _shareQueueLazy.Value;
            }
        }
    }
}
