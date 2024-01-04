using System;
namespace AE_NetWork
{
    public class ByteArray
    {
        //默认大小
        const int DEFAULT_SIZE = 1024;
        //初始大小
        int initSize = 0;
        //缓冲区
        public byte[] bytes;
        //读写位置
        public int readIdx = 0;
        public int writeIdx = 0;
        //容量
        private int capacity = 0;
        //剩余空间
        public int remain { get { return capacity - writeIdx; } }
        //数据长度
        public int length { get { return writeIdx - readIdx; } }


        //构造函数
        public ByteArray(int size = DEFAULT_SIZE)
        {
            bytes = new byte[size];
            capacity = size;
            initSize = size;
            readIdx = 0;
            writeIdx = 0;
        }

        //构造函数
        public ByteArray(byte[] defaultBytes)
        {
            bytes = defaultBytes;
            capacity = defaultBytes.Length;
            initSize = defaultBytes.Length;
            readIdx = 0;
            writeIdx = defaultBytes.Length;
        }

        //重设尺寸
        public void ReSize(int size)
        {
            if (size < length) return;
            if (size < initSize) return;
            int n = 1;
            while (n < size) n *= 2;
            capacity = n;
            byte[] newBytes = new byte[capacity];
            Array.Copy(bytes, readIdx, newBytes, 0, writeIdx - readIdx);
            bytes = newBytes;
            writeIdx = length;
            readIdx = 0;
        }

        //检查并移动数据
        public void CheckAndMoveBytes()
        {
            if (length < 8)
            {
                MoveBytes();
            }
        }

        //移动数据
        public void MoveBytes()
        {
            Array.Copy(bytes, readIdx, bytes, 0, length);
            writeIdx = length;
            readIdx = 0;
        }

        //写入数据
        public int Write(byte[] bs, int offset, int count)
        {
            if (remain < count)
            {
                ReSize(length + count);
            }
            Array.Copy(bs, offset, bytes, writeIdx, count);
            writeIdx += count;
            return count;
        }

        //读取数据
        public int Read(byte[] bs, int offset, int count)
        {
            count = Math.Min(count, length);
            Array.Copy(bytes, 0, bs, offset, count);
            readIdx += count;
            CheckAndMoveBytes();
            return count;
        }

        //读取Int16
        public Int16 ReadInt16()
        {
            if (length < 2) return 0;
            Int16 ret = (Int16)((bytes[1] << 8) | bytes[0]);
            readIdx += 2;
            CheckAndMoveBytes();
            return ret;
        }

        //读取Int32
        public Int32 ReadInt32()
        {
            if (length < 4) return 0;
            Int32 ret = (Int32)((bytes[3] << 24) |
                                 (bytes[2] << 16) |
                                 (bytes[1] << 8) |
                                  bytes[0]);
            readIdx += 4;
            CheckAndMoveBytes();
            return ret;
        }

        //打印缓冲区（仅为调试）
        public override string ToString()
        {
            return BitConverter.ToString(bytes, readIdx, length);
        }

        //打印调试信息（仅为调试）
        public string Debug()
        {
            return string.Format("readIdx({0}) writeIdx({1}) bytes({2})",
                readIdx,
                writeIdx,
                BitConverter.ToString(bytes, 0, bytes.Length)
            );
        }
    }
}