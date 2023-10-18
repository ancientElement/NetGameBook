namespace NetGameRunning{
public class PlayerMessage : AE_NetWork.BaseMessage<NetGameRunning.PlayerData>{
public override int GetMessageID()
{
return 10001;
}public override void WriteIn(byte[] buffer, int beginIndex,int length)
{
 data = NetGameRunning.PlayerData.Parser.ParseFrom(buffer, beginIndex, length);
}
}
}