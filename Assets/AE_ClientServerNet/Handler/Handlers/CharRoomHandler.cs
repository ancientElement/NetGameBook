using AE_ClientNet;
using NetGameRunning;
using UnityEngine;

namespace AE_ClientServerNet
{
    public class CharRoomHandler
    {

        public static void ChatMessageHandler(BaseMessage message, ClientSocket client)
        {
            ChatMessage chatMessage = (ChatMessage)message;
            chatMessage.data.ChatWords = $"[{client.socket.RemoteEndPoint}]" + chatMessage.data.ChatWords;
            client.serverSocket.Broadcast(chatMessage);
            Debug.Log($"转发消息来自:[{client.socket.RemoteEndPoint}]");
        }
        public static void EmptyMessageHandler(BaseMessage arg1, ClientSocket client)
        {
            //Debug.Log("空消息");
            EmptyMessage emptyMessage = (EmptyMessage)arg1;
            client.serverSocket.Broadcast(emptyMessage);
        }
    }
}