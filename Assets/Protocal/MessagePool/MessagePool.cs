namespace AE_ClientNet
{
    public static class MessagePool
    {
        public static int QuitMessage_ID = 1;
        public static int HeartMessage_ID = 2;
        public static int ChatMessage_ID = 10002;
        public static int EmptyMessage_ID = 10003;
        static int[] messageIDs = new int[] { 1, 2, 10002, 10003 };
        public static int[] MessageIDs => messageIDs;
        private static readonly System.Collections.Generic.Dictionary<int, System.Func<BaseMessage>> MessageTypeMap = new System.Collections.Generic.Dictionary<int, System.Func<BaseMessage>>
        {
            {1,() => new NetSystem.QuitMessage()},
            {2,() => new NetSystem.HeartMessage()},
            {10002,() => new NetGameRunning.ChatMessage()},
            {10003,() => new NetGameRunning.EmptyMessage()}
        };
        public static BaseMessage GetMessage(int id)
        {
            if (MessageTypeMap.TryGetValue(id, out System.Func<BaseMessage> messageFactory)) { return messageFactory?.Invoke(); }
            return null;
        }
    }
}
