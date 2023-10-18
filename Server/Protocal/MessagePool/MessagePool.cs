using NetGameRunning;
using NetSystem;

namespace AE_NetWork
{
    public static class MessagePool
    {
        public static int QuitMessage_ID = 1;
        public static int HeartMessage_ID = 2;
        public static int PositionMessage_ID = 10000;
        public static int PlayerMessage_ID = 10001;
        public static int ChatMessage_ID = 10002;
        static int[] messageIDs = new int[] { 1, 2, 10000, 10001, 10002 };
        public static int[] MessageIDs => messageIDs; public static BaseMessage GetMessage(int id)
        {
            if (id == 1) return new QuitMessage();
            if (id == 2) return new HeartMessage();
            if (id == 10000) return new PositionMessage();
            if (id == 10001) return new PlayerMessage();
            if (id == 10002) return new ChatMessage();
            return null;
        }
    }
}
