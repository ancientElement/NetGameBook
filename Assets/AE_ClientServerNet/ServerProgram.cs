using System.Threading;
using UnityEngine;

namespace AE_ClientServerNet
{
    public class ServerProgram
    {
        static string LocalIP = "127.0.0.1";
        static int LocalPoint = 8080;

        public static ServerSocket socket;
        public static bool Started
        {
            get
            {
                if (socket == null) return false;
                return socket.Started;
            }
        }

        public static void Start(string ip, int port)
        {
            if (Started) return;
            LocalIP = ip;
            LocalPoint = port;
            ThreadPool.QueueUserWorkItem(Main);
        }

        public static void Stop()
        {
            socket?.socket.Close();
        }

        static void Main(object? state)
        {
            MainHandler.AddAllListener();

            socket = new ServerSocket();
            socket.Start(LocalIP, LocalPoint, 1024);

            Debug.Log("服务器开启成功");
        }
    }
}