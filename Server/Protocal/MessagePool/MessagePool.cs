using AE_ServerNet;
using NetGameRunning;
using NetSystem;

namespace AE_ServerNet
{
    public static class MessagePool
    {
        public static int QuitMessage_ID = 1;
        public static int HeartMessage_ID = 2;
        public static int ChatMessage_ID = 10002;
        public static int EmptyMessage_ID = 10003;
        static int[] messageIDs = new int[] { 1, 2, 10002, 10003 };
        public static int[] MessageIDs => messageIDs;

        public static BaseMessage GetMessage(int id)
        {
            if (id == 1) return new QuitMessage();
            if (id == 2) return new HeartMessage();
            if (id == 10002) return new ChatMessage();
            if (id == 10003) return new EmptyMessage();
            return null;
        }
    }
}