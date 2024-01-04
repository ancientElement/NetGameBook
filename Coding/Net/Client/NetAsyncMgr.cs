using System;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using NetSystem;
using NetGameRunning;

namespace AE_NetWork
{
    public static class NetAsyncMgr
    {
        private static Socket m_socket;

        public static Socket Socket => m_socket;

        //缓存
        private static byte[] bufferBytes = new byte[1024 * 1024];

        //缓存长度
        private static int bufferLenght;

        //消息队列
        private static Queue<BaseMessage> reciveMessageQueue = new Queue<BaseMessage>();

        private static int MAX_MESSAGE_FIRE = 30;

        //监听消息处理
        private static Dictionary<int, Action<BaseMessage>> listeners = new Dictionary<int, Action<BaseMessage>>();

        private static readonly float heartMessageIntervalTime = 120f;

        private static float heartMessageTimer = 0;

        static NetAsyncMgr()
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
                listeners.Add(item, null);
            }
        }

        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="callback"></param>
        public static void AddNetMessageListener(int messageID, Action<BaseMessage> callback)
        {
            if (listeners.ContainsKey(messageID))
                listeners[messageID] += callback;
            else
                Debug.LogWarning("没有这个消息类型" + messageID);
        }

        /// <summary>
        /// 移除网络消息监听
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="callback"></param>
        public static void RemoveNetMessageListener(int messageID, Action<BaseMessage> callback)
        {
            if (listeners.ContainsKey(messageID))
                listeners[messageID] -= callback;
            else
                Debug.LogWarning("没有这个消息类型" + messageID);
        }

        /// <summary>
        /// 心跳消息缓存
        /// </summary>
        private static HeartMessage HeartMessage;

        /// <summary>
        /// 连接状态
        /// </summary>
        private static bool isConnected;
        public static bool IsConnected => isConnected;

        /// <summary>
        /// Tick
        /// </summary>
        public static void Update()
        {
            heartMessageTimer += Time.deltaTime;

            MsgUpdate();

            if (heartMessageTimer == heartMessageIntervalTime)
            {
                SendHeartMessage();
                heartMessageTimer = 0;
            }
        }

        /// <summary>
        /// 更新消息
        /// </summary>
        public static void MsgUpdate()
        {
            //初步判断，提升效率
            if (reciveMessageQueue.Count == 0)
            {
                return;
            }

            //重复处理消息
            for (int i = 0; i < MAX_MESSAGE_FIRE; i++)
            {
                //获取第一条消息
                BaseMessage msgBase = null;
                lock (reciveMessageQueue)
                {
                    if (reciveMessageQueue.Count > 0)
                    {
                        msgBase = reciveMessageQueue.Dequeue();
                    }
                }

                //分发消息
                if (msgBase != null)
                {
                    listeners[msgBase.GetMessageID()]?.Invoke(msgBase);
                }
                //没有消息了
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 销毁时关闭连接
        /// </summary>
        public static void OnDestroy()
        {
            if (isConnected == true)
                Close(true);
        }

        /// <summary>
        /// 设置最大事件分发
        /// </summary>
        /// <param name="value"></param>
        public static void SetMaxMessageFire(int value)
        {
            MAX_MESSAGE_FIRE = value;
        }

        /// <summary>
        /// 发送心跳消息
        /// </summary>
        private static void SendHeartMessage()
        {
            if (HeartMessage == null)
                HeartMessage = new HeartMessage();
            Send(HeartMessage);
            Debug.Log("发送心跳消息");
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public static void Connect(string host, int port)
        {
            IPEndPoint SeveriPEndPoint = new IPEndPoint(IPAddress.Parse(host), port);
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            SocketAsyncEventArgs argsConnect = new SocketAsyncEventArgs();
            argsConnect.RemoteEndPoint = SeveriPEndPoint;

            argsConnect.Completed += (socket, args1) =>
            {
                if (args1.SocketError == SocketError.Success)
                {
                    Debug.Log($"连接成功: {host}:{port}");
                    isConnected = true;
                    SendHeartMessage();
                    //接收消息
                    SocketAsyncEventArgs argsRecive = new SocketAsyncEventArgs();
                    argsRecive.SetBuffer(bufferBytes, 0, bufferBytes.Length);
                    argsRecive.Completed += Recive;
                    m_socket.ReceiveAsync(argsRecive);
                }
                else
                {
                    Debug.Log($"连接失败:{args1.SocketError}");
                }
            };
            m_socket.ConnectAsync(argsConnect);
        }

        /// <summary>
        /// 接受消息
        /// </summary>
        private static void Recive(object socket, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                int bytesLength = args.BytesTransferred;

                HandleReceiveMessage(bytesLength);

                //接收消息
                if (socket != null && m_socket.Connected && isConnected)
                    args.SetBuffer(bufferLenght, bufferBytes.Length);
                m_socket.ReceiveAsync(args);
            }
            else
            {
                Debug.Log($"{args.SocketError}");
                if (isConnected == true)
                    Close();
            }
        }

        /// <summary>
        /// 处理接受消息  推进消息队列
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="reciveLength"></param>
        private static void HandleReceiveMessage(int reciveLength)
        {
            try
            {
                if (reciveLength == 0) return;

                //处理
                int massageID = -1;
                int messageBodyLength = 0;
                int currentIndex = 0;


                bufferLenght += reciveLength;

                while (true) //粘包
                {
                    if (bufferLenght >= 8)
                    {
                        //ID
                        massageID = BitConverter.ToInt32(bufferBytes, currentIndex);
                        currentIndex += 4;
                        //长度
                        messageBodyLength = BitConverter.ToInt32(bufferBytes, currentIndex) - 8;
                        currentIndex += 4;

                        // if (massageID == MessagePool.ChatMessage_ID)
                        // {
                        //     Debug.Log("");
                        // }
                    }

                    if (bufferLenght - currentIndex >= messageBodyLength && massageID != -1)
                    {
                        //消息体 
                        BaseMessage baseMassage = MessagePool.GetMessage(massageID);
                        baseMassage.WriteIn(bufferBytes, currentIndex, messageBodyLength);

                        reciveMessageQueue.Enqueue(baseMassage);

                        currentIndex += messageBodyLength;
                        if (currentIndex == bufferLenght)
                        {
                            bufferLenght = 0;
                            break;
                        }
                    }
                    else //分包
                    {
                        Array.Copy(bufferBytes, currentIndex - 8, bufferBytes, 0, bufferLenght - currentIndex + 8);
                        bufferLenght = bufferLenght - currentIndex + 8;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"消息解析出错: {e.Message}");
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="state"></param>
        public static void Send(BaseMessage info)
        {
            if (m_socket != null && m_socket.Connected && isConnected)
            {
                byte[] bytes = info.GetBytes();

                SocketAsyncEventArgs argsSend = new SocketAsyncEventArgs();
                argsSend.SetBuffer(bytes, 0, bytes.Length);
                argsSend.Completed += (socket, args) =>
                {
                    if (args.SocketError == SocketError.Success)
                    {
                    }
                    else
                    {
                        Debug.Log($"{args.SocketError}");
                        Close();
                    }
                };
                m_socket.SendAsync(argsSend);
            }
            else
            {
                if (isConnected == true)
                    Close();
            }
        }

        /// <summary>
        /// 测试发送
        /// </summary>
        /// <param name="bytes"></param>
        public static void SendTest(byte[] bytes)
        {
            m_socket.Send(bytes);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        private static void Close(bool isSelf = false)
        {
            if (m_socket != null)
            {
                isConnected = false;

                Debug.Log("断开连接");

                m_socket.Send(new QuitMessage().GetBytes());
                m_socket.Shutdown(SocketShutdown.Both);
                m_socket.Disconnect(false);
                m_socket.Close();

                m_socket = null;
            }

            if (!isSelf)
            {
                //重连
            }
        }
    }
}