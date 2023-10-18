using NetGameRunning;

namespace TeachTCPAsync
{
    internal class Program
    {
        static readonly string LocalIP = "127.0.0.1";
        static readonly int LocalPoint = 8080;

        public static ServerSocket socket;

        static void Main(string[] args)
        {
            MainHandler.AddAllListener();

            socket = new ServerSocket();
            socket.Start(LocalIP, LocalPoint, 1024);

            Console.WriteLine("服务器开启成功");

            while (true)
            {
                string input = Console.ReadLine();
                if (input == null || input.Length == 0) continue;
                //定义规则
                if (input == "Quit")
                {
                    //socket.Close();
                    break;
                }

                if (input.Substring(0, 2) == "B:")
                {
                    if (input.Substring(2) == "0")
                    {
                        PlayerMessage playerMassage = new PlayerMessage()
                        {
                            data = new PlayerData()
                            {
                                Position = new PositionData()
                                {
                                    X = 1000,
                                    Y = 1000,
                                }
                            }
                        };
                        socket.Broadcast(playerMassage);
                    }
                }

                System.Threading.Thread.Sleep(1);//让程序挂起1毫秒，这样做的目的是避免死循环，让CPU有个短暂的喘息时间。
            }
        }
    }
}