using AE_ServerNet;

namespace NetSystem
{
    public class HeartMessage : BaseSystemMessage
    {
        public override int GetMessageID()
        {
            return 2;
        }
    }
}