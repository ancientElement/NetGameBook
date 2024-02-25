using System.Threading;
using UnityEngine;

namespace AE_ClientServerNet
{
    public class Program
    {
        static string LocalIP = "127.0.0.1";
        static int LocalPoint = 8080;

        public static ServerSocket socket;

        public static bool Started;

        public static void Start(string ip, int port)
        {
            if (Started) return;
            LocalIP = ip;
            LocalPoint = port;
            ThreadPool.QueueUserWorkItem(Main);
            Started = true;
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