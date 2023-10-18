namespace NetGameRunning{
public class PositionMessage : AE_NetWork.BaseMessage<NetGameRunning.PositionData>{
public override int GetMessageID()
{
return 10000;
}public override void WriteIn(byte[] buffer, int beginIndex,int length)
{
 data = NetGameRunning.PositionData.Parser.ParseFrom(buffer, beginIndex, length);
}
}
}