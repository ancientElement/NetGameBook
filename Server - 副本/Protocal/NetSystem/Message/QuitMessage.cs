using AE_ServerNet;

namespace NetSystem
{
    public class QuitMessage : BaseSystemMessage
    {
        public override int GetMessageID()
        {
            return 1;
        }
    }
}