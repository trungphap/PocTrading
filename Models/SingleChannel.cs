using System;
using System.Collections.Concurrent;
using System.Threading.Channels;
using System.Threading.Tasks;
namespace Models
{
    public sealed class SingleChannel

    {
        private static readonly Channel <Shape> _shareChannelLazy =  Channel.CreateBounded<Shape>(int.MaxValue);
        private SingleChannel()
        {

        }
        static SingleChannel()
        {

        }

        public static ChannelWriter<Shape> ShareChannelWriter
        {
            get
            {
                return _shareChannelLazy.Writer;
            }
        }
        public static ChannelReader<Shape> ShareChannelReader
        {
            get
            {
                return _shareChannelLazy.Reader;
            }
        }
    }
}
