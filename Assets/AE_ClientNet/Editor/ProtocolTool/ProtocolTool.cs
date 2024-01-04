using System.Xml;
using UnityEditor;

namespace AE_ClientNet
{
    public class ProtocolTool
    {
        public static void GenerateCSharpMessage(string xmlPath)
        {
            GenerateCSharp.GenerateMessage(GetNodeList("message", xmlPath));
            AssetDatabase.Refresh();
        }

        public static void GenerateCSharpMessagePool(string xmlPath)
        {
            GenerateCSharp.GenerateMessagePool(GetNodeList("message", xmlPath));
            AssetDatabase.Refresh();
        }

        private static XmlNodeList GetNodeList(string name, string xmlPath)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);
            XmlNode root = xml.SelectSingleNode("messages");
            return root.SelectNodes(name);
        }
    }
}
