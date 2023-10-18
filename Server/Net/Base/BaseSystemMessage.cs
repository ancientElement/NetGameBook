namespace AE_NetWork
{
    public abstract class BaseSystemMessage : BaseMessage<Google.Protobuf.IMessage>
    {
        public override int GetByteLength()
        {
            return 8;
        }
    }
}