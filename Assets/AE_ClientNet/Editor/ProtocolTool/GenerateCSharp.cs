using System.IO;
using System.Xml;
using UnityEngine;

namespace AE_ClientNet
{
    public class GenerateCSharp
    {
        public static void GenerateMessage(XmlNodeList nodeList)
        {
            //字段名
            foreach (XmlNode messageNode in nodeList)
            {
                //消息id
                string messageID = messageNode.Attributes["id"].Value;

                //是否是系统消息
                string systemMessage = messageNode.Attributes["systemMessage"].Value;

                //命名空间
                string namespaceStr = messageNode.Attributes["namespace"].Value;
                //类名字
                string classNameStr = messageNode.Attributes["name"].Value;
                //data类型
                string dataType = messageNode.Attributes["datatype"]?.Value;

                //GetMessageID
                string GetMessageIDMethod = $"public override int GetMessageID()\r\n" +
                     "{\r\n" +
                        $"return {messageID};" +
                      "\r\n}";

                //WriteIn
                string WriteInMethod = $"public override void WriteIn(byte[] buffer, int beginIndex,int length)\r\n" +
                     "{\r\n" +
                        $" data = {dataType}.Parser.ParseFrom(buffer, beginIndex, length);" +
                      "\r\n}";

                string messageStr;

                if (systemMessage != null && systemMessage == "1")
                {
                    //所有数据
                    messageStr = $"namespace {namespaceStr}" +
                                    "{\r\n" +
                                        $"public class {classNameStr} : AE_ClientNet.BaseSystemMessage" +
                                         "{\r\n" +
                                           $"{GetMessageIDMethod}" +
                                           "\r\n}" +
                                    "\r\n}";
                }
                else
                {
                    //所有数据
                    messageStr = $"namespace {namespaceStr}" +
                                    "{\r\n" +
                                        $"public class {classNameStr} : AE_ClientNet.BaseMessage<{dataType}>" +
                                         "{\r\n" +
                                           $"{GetMessageIDMethod}" +
                                           $"{WriteInMethod}" +
                                           "\r\n}" +
                                    "\r\n}";
                }


                GenerateFileTool.Generate($"{Application.dataPath}/Protocal/{namespaceStr}/Message/", $"{classNameStr}.cs", messageStr);
            }
        }

        public static void GenerateMessagePool(XmlNodeList nodeList)
        {
            string messageIDs = "static int[] messageIDs = new int[] {";

            string MessageTypeMap = " private static readonly System.Collections.Generic.Dictionary<int, System.Func<BaseMessage>> MessageTypeMap = new System.Collections.Generic.Dictionary<int, System.Func<BaseMessage>>\r\n        {\r\n";

            string GetMessage = "public static BaseMessage GetMessage(int id) " +
                                "{" +
                                "       if (MessageTypeMap.TryGetValue(id, out System.Func<BaseMessage> messageFactory)) {  " +
                                "                   return messageFactory?.Invoke(); " +
                                "       } " +
                                "       return null;   " +
                                "}\r\n";

            string messageLinkID = string.Empty;

            string str = "namespace AE_ClientNet\r\n" +
                        "{\r\n" +
                            "public static class MessagePool\r\n" +
                            "{\r\n";

            for (int i = 0; i < nodeList.Count; i++)
            {
                string classNameStr = nodeList[i].Attributes["name"].Value;
                string messageID = nodeList[i].Attributes["id"].Value;
                string temp_namespace = nodeList[i].Attributes["namespace"].Value;

                //messageIDs
                if (i == nodeList.Count - 1)
                {
                    messageIDs += messageID;
                    MessageTypeMap += $"{{{messageID},() => new {temp_namespace}.{classNameStr}()}}\r\n";
                }
                else
                {
                    messageIDs += messageID + ",";
                    MessageTypeMap += $"{{{messageID},() => new {temp_namespace}.{classNameStr}()}},\r\n";
                }

                //GetMessage

                //messageLinkID
                messageLinkID += $"public static int {classNameStr}_ID = {messageID};\r\n";
            }

            messageIDs += "};\r\n" + "public static int[] MessageIDs => messageIDs;\r\n";

            MessageTypeMap += "};\r\n";

            str += messageLinkID + messageIDs + MessageTypeMap + GetMessage + "}\r\n}\r\n";

            string dirPath = $"{Application.dataPath}/Protocal/MessagePool/";
            string fileName = $"MessagePool.cs";

            GenerateFileTool.Generate(dirPath, fileName, str);
        }
    }
}
