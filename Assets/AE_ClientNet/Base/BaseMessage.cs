using System;
using Google.Protobuf;

namespace AE_ClientNet
{
    public abstract class BaseMessage
    {
        public abstract int GetByteLength();

        public abstract byte[] GetBytes();

        public abstract int GetMessageID();

        public abstract void WriteIn(byte[] buffer, int beginIndex,int length);
    }

    public abstract class BaseMessage<T> : BaseMessage where T : Google.Protobuf.IMessage,new()
    {
        public T data = new T();

        public override int GetByteLength()
        {
            return 8 + (data == null ? 0 : data.CalculateSize());
        }

        public override byte[] GetBytes()
        {
            byte[] buffer = new byte[GetByteLength()];
            BitConverter.GetBytes(GetMessageID()).CopyTo(buffer, 0);
            BitConverter.GetBytes(GetByteLength()).CopyTo(buffer, 4);
            if (buffer.Length > 8)
                data.ToByteArray().CopyTo(buffer, 8);
            return buffer;
        }

        public override int GetMessageID()
        {
            throw new NotImplementedException();
        }

        public override void WriteIn(byte[] buffer, int beginIndex,int length)
        {
            throw new NotImplementedException();
        }
    }
}