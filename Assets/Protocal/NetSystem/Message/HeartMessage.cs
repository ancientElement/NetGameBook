namespace NetSystem{
public class HeartMessage : AE_ClientNet.BaseSystemMessage{
public override int GetMessageID()
{
return 2;
}
}
}