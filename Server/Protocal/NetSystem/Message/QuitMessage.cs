namespace NetSystem{
public class QuitMessage : AE_NetWork.BaseSystemMessage{
public override int GetMessageID()
{
return 1;
}
}
}