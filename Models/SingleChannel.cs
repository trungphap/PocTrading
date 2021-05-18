using System;
using System.Collections.Concurrent;
using System.Threading.Channels;
using System.Threading.Tasks;
namespace Models
{
    public sealed class SingleChannel

    {
        private static readonly Channel <Shape> _shareChannel =  Channel.CreateBounded<Shape>(int.MaxValue);
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
                return _shareChannel.Writer;
            }
        }
        public static ChannelReader<Shape> ShareChannelReader
        {
            get
            {
                return _shareChannel.Reader;
            }
        }
    }
}
