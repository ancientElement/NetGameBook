using AE_NetWork;
using NetGameRunning;

namespace TeachTCPAsync
{
    public static class MainHandler
    {
        public static void AddAllListener()
        {
            ClientSocket.AddListener(MessagePool.HeartMessage_ID, HeartMessageHandler);
            ClientSocket.AddListener(MessagePool.ChatMessage_ID, ChatMessageHandler);
            ClientSocket.AddListener(MessagePool.EmptyMessage_ID, EmptyMessageHandler);
        }

        private static void ChatMessageHandler(BaseMessage message, ClientSocket client)
        {
            ChatMessage chatMessage = (ChatMessage)message;
            chatMessage.data.ChatWords = $"[{client.socket.RemoteEndPoint}]" + chatMessage.data.ChatWords;
            Program.socket.Broadcast(chatMessage);
            Console.WriteLine($"转发消息来自:[{client.socket.RemoteEndPoint}]");
        }

        private static void HeartMessageHandler(BaseMessage arg1, ClientSocket client)
        {
            Console.WriteLine($"接收到心跳消息:[{client.socket.RemoteEndPoint}]");
        }

        private static void EmptyMessageHandler(BaseMessage arg1,ClientSocket client) {
            //Console.WriteLine("空消息");
            EmptyMessage emptyMessage = (EmptyMessage)arg1;
            Program.socket.Broadcast(emptyMessage);
        }
    }
}
