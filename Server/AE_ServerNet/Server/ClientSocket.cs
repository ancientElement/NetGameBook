using System.Net.Sockets;

namespace AE_ServerNet
{
    public class ClientSocket
    {
        #region Static
        private static Dictionary<int, Action<BaseMessage, ClientSocket>> listeners = new Dictionary<int, Action<BaseMessage, ClientSocket>>();

        private static Action<ClientSocket> Timer;

        private static int TimerInterval = 1000;

        public static readonly float TimeOutTime = 1120f;

        private static int CLIENT_BEGIN_ID = 1;

        static ClientSocket()
        {
            InitLister();
        }

        /// <summary>
        /// 初始化消息监听器
        /// </summary>
        private static void InitLister()
        {
            foreach (int item in MessagePool.MessageIDs)
            {
                if (listeners.ContainsKey(item)) { listeners[item] = null; continue; }
                listeners.Add(item, null);
            }
        }

        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="callback"></param>
        public static void AddListener(int messageID, Action<BaseMessage, ClientSocket> callback)
        {
            if (listeners.ContainsKey(messageID))
                listeners[messageID] += callback;
            else
                Console.WriteLine("没有这个消息类型" + messageID);
        }

        ///// <summary>
        ///// 添加计时器回调
        ///// </summary>
        //public static void AddTimerListener(Action<ClientSocket> callback) { Timer += callback; }

        #endregion

        public int clientID;

        public Socket socket;

        public bool Connected => socket.Connected;

        //缓存
        private byte[] bufferBytes = new byte[1024 * 1024];

        //接收缓冲区
        ByteArray readBuff = new ByteArray();

        //缓存长度
        private int bufferLenght = 0;

        private long lastHeartMessageTime = -1;

        //监听消息处理
        public long LastHeartMessageTime { get => lastHeartMessageTime; set { lastHeartMessageTime = value; } }

        //发送缓存
        Queue<ByteArray> writeQueue = new Queue<ByteArray>();

        public ClientSocket(Socket socket)
        {
            this.clientID = CLIENT_BEGIN_ID;
            this.socket = socket;
            ++CLIENT_BEGIN_ID;

            this.socket.BeginReceive(bufferBytes, bufferLenght, bufferBytes.Length - bufferLenght, SocketFlags.None, ReciveCallback, null);

            //socket.BeginReceive(readBuff.bytes, readBuff.writeIdx,
            //readBuff.remain, 0, Receive, socket);
        }

        private void ReciveCallback(IAsyncResult result)
        {
            try
            {
                if (this.socket != null && this.socket.Connected)
                {
                    int byteLength = this.socket.EndReceive(result);

                    HandleReceiveMessage(byteLength, () =>
                    {
                        this.socket.BeginReceive(bufferBytes, bufferLenght, bufferBytes.Length - bufferLenght, SocketFlags.None, ReciveCallback, null);
                    });
                }
                else
                {
                    Console.WriteLine("没有连接，不用再收消息了");
                    Program.socket.CloseClientSocket(this);
                }
            }
            catch (Exception e)
            {
                if (e is SocketException)
                {
                    Console.WriteLine($"接收消息出错 [{socket.RemoteEndPoint}] {(e as SocketException).ErrorCode}:{e.Message}");
                }
                else
                {
                    Console.WriteLine($"接收消息出错 [{socket.RemoteEndPoint}] :{e.Message}");
                }
                Program.socket.CloseClientSocket(this);
            }
        }

        private void HandleReceiveMessage(int bytesLength, Action callback)
        {
            byte[] bytes = readBuff.bytes;

            if (bytesLength == 0) return;

            //处理
            int massageID = -1;
            int massageBodyLength = -1;
            int currentIndex = 0;

            bufferLenght += bytesLength;

            while (true)//粘包
            {
                if (bufferLenght >= 8)
                {
                    //ID
                    massageID = BitConverter.ToInt32(bufferBytes, currentIndex);
                    currentIndex += 4;
                    //长度
                    massageBodyLength = BitConverter.ToInt32(bufferBytes, currentIndex) - 8;
                    currentIndex += 4;
                }

                if (bufferLenght - currentIndex >= massageBodyLength && massageBodyLength != -1 && massageID != -1)
                {
                    //消息体 
                    BaseMessage baseMassage = MessagePool.GetMessage(massageID);

                    if (baseMassage != null)
                    {
                        if (massageBodyLength != 0)
                            baseMassage.WriteIn(bufferBytes, currentIndex, massageBodyLength);

                        ThreadPool.QueueUserWorkItem(HandleMassage, baseMassage);
                    }

                    currentIndex += massageBodyLength;
                    if (currentIndex == bufferLenght)
                    {
                        bufferLenght = 0;
                        break;
                    }
                }
                else//分包
                {
                    if (massageBodyLength != -1)
                        currentIndex -= 8;
                    Array.Copy(bufferBytes, currentIndex, bufferBytes, 0, bufferLenght - currentIndex);
                    bufferLenght = bufferLenght - currentIndex;
                    break;
                }
            }

            //继续接收
            callback?.Invoke();
        }

        private void HandleMassage(object? state)
        {
            if (state == null)
            {
                Console.WriteLine($"接收消息出错: 消息内容为null");
                return;
            }

            BaseMessage message = state as BaseMessage;

            if (message == null)
            {
                Console.WriteLine($"接收消息出错: 消息内容为null");
                return;
            }

            listeners[message.GetMessageID()]?.Invoke(message, this);
        }

        public void Send(BaseMessage info)
        {
            if (!Connected)
            {
                Program.socket.CloseClientSocket(this);
                return;
            }
            try
            {
                this.socket.BeginSend(info.GetBytes(), 0, info.GetByteLength(), SocketFlags.None, SendCallback, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"发送消息出错: {e.Message}");
                Program.socket.CloseClientSocket(this);
            }
        }

        private void SendCallback(IAsyncResult result)
        {
            try
            {
                this.socket.EndSend(result);
            }
            catch (SocketException e)
            {
                Console.WriteLine($"发送消息出错 {e.SocketErrorCode}:{e.Message}");
            }
        }


        public void Close()
        {
            if (Connected)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}
