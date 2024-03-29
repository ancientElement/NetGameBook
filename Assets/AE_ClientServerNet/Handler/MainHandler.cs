﻿using AE_ClientNet;
using UnityEngine;

namespace AE_ClientServerNet
{
    public static class MainHandler
    {
        public static void AddAllListener()
        {
            ClientSocket.AddListener(MessagePool.HeartMessage_ID, HeartMessageHandler);
            ClientSocket.AddListener(MessagePool.ChatMessage_ID, CharRoomHandler.ChatMessageHandler);
            ClientSocket.AddListener(MessagePool.EmptyMessage_ID, CharRoomHandler.EmptyMessageHandler);
        }

        private static void HeartMessageHandler(BaseMessage arg1, ClientSocket client)
        {
            Debug.Log($"接收到心跳消息:[{client.socket.RemoteEndPoint}]");
        }
    }
}
