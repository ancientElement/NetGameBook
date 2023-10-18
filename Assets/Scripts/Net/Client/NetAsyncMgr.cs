using System;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

using NetSystem;
using NetGameRunning;

namespace AE_NetWork
{
    public class NetAsyncMgr : MonoBehaviour
    {
        private static NetAsyncMgr _instance;
        public static NetAsyncMgr Instance => _instance;

        public Socket socket;

        //缓存
        private byte[] bufferBytes = new byte[1024 * 1024];

        //缓存长度
        private int bufferLenght;

        //消息队列
        private Queue<BaseMessage> reciveMessageQueue = new Queue<BaseMessage>();

        //监听消息处理
        private static Dictionary<int, Action<BaseMessage>> listeners = new Dictionary<int, Action<BaseMessage>>();

        private static readonly float HeartMessageIntervalTime = 120f;

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
        public static void AddListener(int messageID, Action<BaseMessage> callback)
        {
            if (listeners.ContainsKey(messageID))
                listeners[messageID] += callback;
            else
                Debug.LogWarning("没有这个消息类型" + messageID);
        }

        private HeartMessage HeartMessage;

        private bool isConnected;

        public bool IsConnected => isConnected;

        private void Awake()
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);

            //发送心跳消息
            InvokeRepeating(nameof(SendHeartMessage), HeartMessageIntervalTime, HeartMessageIntervalTime);
        }

        private void Update()
        {
            if (reciveMessageQueue.Count > 0)
            {
                BaseMessage message = reciveMessageQueue.Dequeue();
                if (message != null)
                {
                    listeners[message.GetMessageID()]?.Invoke(message);
                }
                else
                {
                    Console.WriteLine($"消息处理出错");
                }
            }
        }

        private void OnDestroy()
        {
            if (isConnected == true)
                Close(true);
        }

        /// <summary>
        /// 发送心跳消息
        /// </summary>
        private void SendHeartMessage()
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
        public void Connect(string host, int port)
        {
            IPEndPoint SeveriPEndPoint = new IPEndPoint(IPAddress.Parse(host), port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            SocketAsyncEventArgs argsConnect = new SocketAsyncEventArgs();
            argsConnect.RemoteEndPoint = SeveriPEndPoint;

            argsConnect.Completed += (socket, args1) =>
            {
                if (args1.SocketError == SocketError.Success)
                {
                    print($"连接成功: {host}:{port}");
                    isConnected = true;

                    //接收消息
                    SocketAsyncEventArgs argsRecive = new SocketAsyncEventArgs();
                    argsRecive.SetBuffer(bufferBytes, 0, bufferBytes.Length);
                    argsRecive.Completed += Recive;
                    this.socket.ReceiveAsync(argsRecive);
                }
                else
                {
                    print($"连接失败:{args1.SocketError}");
                }
            };
            this.socket.ConnectAsync(argsConnect);
        }

        /// <summary>
        /// 接受消息
        /// </summary>
        private void Recive(object socket, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                int bytesLength = args.BytesTransferred;

                HandleReceiveMessage(bytesLength);

                //接收消息
                if (socket != null && this.socket.Connected && isConnected)
                    args.SetBuffer(bufferLenght, bufferBytes.Length);
                this.socket.ReceiveAsync(args);
            }
            else
            {
                print($"{args.SocketError}");
                if (isConnected == true)
                    Close();
            }
        }

        /// <summary>
        /// 处理接受消息  推进消息队列
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="reciveLength"></param>
        private void HandleReceiveMessage(int reciveLength)
        {
            try
            {
                if (reciveLength == 0) return;

                //处理
                int massageID = -1;
                int messageBodyLength = 0;
                int currentIndex = 0;

                bufferLenght += reciveLength;

                while (true)//粘包
                {
                    if (bufferLenght >= 8)
                    {
                        //ID
                        massageID = BitConverter.ToInt32(bufferBytes, currentIndex);
                        currentIndex += 4;
                        //长度
                        messageBodyLength = BitConverter.ToInt32(bufferBytes, currentIndex) - 8;
                        currentIndex += 4;
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
                    else//分包
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
        public void Send(BaseMessage info)
        {
            if (socket != null && this.socket.Connected && isConnected)
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
                        print($"{args.SocketError}");
                        Close();
                    }
                };
                this.socket.SendAsync(argsSend);

            }
            else
            {
                if (isConnected == true)
                    Close();
            }
        }

        public void SendTest(byte[] bytes)
        {
            socket.Send(bytes);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        private void Close(bool isSelf = false)
        {
            if (socket != null)
            {
                isConnected = false;

                print("断开连接");

                socket.Send(new QuitMessage().GetBytes());
                socket.Shutdown(SocketShutdown.Both);
                socket.Disconnect(false);
                socket.Close();

                socket = null;
            }

            if (!isSelf)
            {
                //重连
            }
        }
    }
}