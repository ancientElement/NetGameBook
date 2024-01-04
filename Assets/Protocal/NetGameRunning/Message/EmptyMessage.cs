namespace NetGameRunning{
public class EmptyMessage : AE_ClientNet.BaseMessage<NetGameRunning.EmptyMessageData>{
public override int GetMessageID()
{
return 10003;
}public override void WriteIn(byte[] buffer, int beginIndex,int length)
{
 data = NetGameRunning.EmptyMessageData.Parser.ParseFrom(buffer, beginIndex, length);
}
}
}