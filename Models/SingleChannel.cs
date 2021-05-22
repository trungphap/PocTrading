using System;
using System.Threading.Channels;
namespace Models
{
    public class SingleChannel

    {
        private static Channel<Shape> _shareChannel;
        private static Object _locker = new Object();
        private SingleChannel()
        {

        }
        static SingleChannel()
        {

        }
        public static void SetChannel(int length)
        {
            //_shareChannel = Channel.CreateBounded<Shape>(length);
            if (_shareChannel == null)
            {
                lock (_locker)
                {
                    if (_shareChannel == null) _shareChannel = Channel.CreateBounded<Shape>(length);
                }
            }
        }
        public static void ResetChannel()
        {
            if (_shareChannel != null)
            {
                lock (_locker)
                {
                    if (_shareChannel != null) _shareChannel =null;
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
