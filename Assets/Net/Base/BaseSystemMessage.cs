using System;

namespace AE_NetWork
{
    public abstract class BaseSystemMessage: BaseMessage
    {
        public override int GetByteLength()
        {
            return 8;
        }
        public override byte[] GetBytes()
        {
            byte[] buffer = new byte[GetByteLength()];
            BitConverter.GetBytes(GetMessageID()).CopyTo(buffer, 0);
            BitConverter.GetBytes(GetByteLength()).CopyTo(buffer, 4);
            return buffer;
        }

        public override int GetMessageID()
        {
            throw new NotImplementedException();
        }

        public override void WriteIn(byte[] buffer, int beginIndex, int length)
        {
            throw new NotImplementedException();
        }
    }
}