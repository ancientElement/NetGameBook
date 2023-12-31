using AE_ServerNet;

namespace NetGameRunning
{
    public class EmptyMessage : BaseMessage<NetGameRunning.EmptyMessageData>
    {
        public override int GetMessageID()
        {
            return 10003;
        }
        public override void WriteIn(byte[] buffer, int beginIndex, int length)
        {
            data = NetGameRunning.EmptyMessageData.Parser.ParseFrom(buffer, beginIndex, length);
        }
    }
}