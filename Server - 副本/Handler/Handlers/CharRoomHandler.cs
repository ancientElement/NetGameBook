using AE_ServerNet;
using NetGameRunning;

namespace AE_ServerNet
{
    public class CharRoomHandler
    {
        public static object ServerProgram { get; private set; }

        public static void ChatMessageHandler(BaseMessage message, ClientSocket client)
        {
            ChatMessage chatMessage = (ChatMessage)message;
            chatMessage.data.ChatWords = $"[{client.socket.RemoteEndPoint}]" + chatMessage.data.ChatWords;
            Program.socket.Broadcast(chatMessage);
            Console.WriteLine($"转发消息来自:[{client.socket.RemoteEndPoint}]");
        }
        public static void EmptyMessageHandler(BaseMessage arg1, ClientSocket client)
        {
            //Console.WriteLine("空消息");
            EmptyMessage emptyMessage = (EmptyMessage)arg1;
            Program.socket.Broadcast(emptyMessage);
        }
    }
}