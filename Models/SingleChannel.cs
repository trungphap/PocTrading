using System;
using System.Collections.Concurrent;
using System.Threading.Channels;
using System.Threading.Tasks;
namespace Models
{
    public sealed class SingleChannel

    {
        public static int Length { set; get; }
        private static Channel <Shape> _shareChannel ;
        private static Object _locker = new Object() ;
        private SingleChannel()
        {

        }
        static SingleChannel()
        {

        }
        public static void SetChannel(int Length)
        {
            if(_shareChannel == null)
            {
                lock (_locker)
                {
                    if (_shareChannel == null) _shareChannel = Channel.CreateBounded<Shape>(Length);
                }
            }            
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
