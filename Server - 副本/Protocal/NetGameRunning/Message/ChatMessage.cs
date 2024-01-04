using AE_ServerNet;

namespace AE_ServerNet
{
    public class ChatMessage : BaseMessage<NetGameRunning.GlobalChatData>
    {
        public override int GetMessageID()
        {
            return 10002;
        }
        public override void WriteIn(byte[] buffer, int beginIndex, int length)
        {
            data = NetGameRunning.GlobalChatData.Parser.ParseFrom(buffer, beginIndex, length);
        }
    }
}