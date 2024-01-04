namespace NetGameRunning{
public class ChatMessage : AE_ClientNet.BaseMessage<NetGameRunning.GlobalChatData>{
public override int GetMessageID()
{
return 10002;
}public override void WriteIn(byte[] buffer, int beginIndex,int length)
{
 data = NetGameRunning.GlobalChatData.Parser.ParseFrom(buffer, beginIndex, length);
}
}
}