namespace NetSystem{
public class HeartMessage : AE_NetWork.BaseSystemMessage{
public override int GetMessageID()
{
return 2;
}
}
}