using System.IO;
using UnityEditor;
using UnityEngine;

namespace AE_ClientNet
{

    public class ProtocolBufferToolWindow : EditorWindow
    {
        public ProtocolGenrateData data;

        private int lauage;

        [MenuItem("Protobuf/配置生成Window")]
        public static void ShowWindow()
        {
            ProtocolBufferToolWindow window = GetWindowWithRect<ProtocolBufferToolWindow>(new Rect(0, 0, 550, 330));
            window.Show();
        }

        private void OnEnable()
        {
            data = new ProtocolGenrateData();
        }

        private void OnGUI()
        {
            float height = 0;

            GUI.Label(new Rect(0, 0, 150, 30), "proto文件夹位置");
            Rect rect = new Rect(150, 0, 400, 30);
            data.protoFilePath = GUI.TextField(rect, data.protoFilePath);
            if (GUI.Button(new Rect(560, height, 100, 30), "点击选择文件夹"))
            {
                data.protoFilePath = EditorUtility.OpenFolderPanel("proto文件夹位置", data.protoFilePath, "");
            }

            if (Event.current.type == EventType.DragExited && rect.Contains(Event.current.mousePosition))
            {
                UnityEngine.Object[] objs = DragAndDrop.objectReferences;
                data.protoFilePath = Directory.GetCurrentDirectory() + "\\" + AssetDatabase.GetAssetPath(objs[0]);
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            }

            height += 35;

            GUI.Label(new Rect(0, height, 150, 30), "输出位置");
            rect = new Rect(150, height, 400, 30);
            data.outputFilePath = GUI.TextField(rect, data.outputFilePath);
            if (GUI.Button(new Rect(560, height, 100, 30), "点击选择文件夹"))
            {
                data.outputFilePath = EditorUtility.OpenFolderPanel("输出位置", data.outputFilePath, "");
            }

            height += 35;

            if (Event.current.type == EventType.DragExited && new Rect(150, height, 400, 30).Contains(Event.current.mousePosition))
            {
                UnityEngine.Object[] objs = DragAndDrop.objectReferences;
                data.outputFilePath = Directory.GetCurrentDirectory() + "\\" + AssetDatabase.GetAssetPath(objs[0]);
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            }

            GUI.Label(new Rect(0, height, 150, 30), "proto.exe位置");
            data.protoExepath = GUI.TextField(new Rect(150, height, 400, 30), data.protoExepath);
            if (GUI.Button(new Rect(560, height, 100, 30), "点击选择文件"))
            {
                data.protoExepath = EditorUtility.OpenFilePanel("proto.exe位置", data.protoExepath, "exe");
            }

            height += 35;

            lauage = GUI.Toolbar(new Rect(0, height, 550, 50), lauage, new string[] { "C#", "Java", "C++" });
            height += 55;

            if (GUI.Button(new Rect(0, height, 550, 50), "生成Proto类"))
            {
                if (data.protoFilePath != string.Empty)
                    switch (lauage)
                    {
                        case 0:
                            ProtobufTool.Generate(data.outputFilePath, data.protoFilePath, ProtobufTool.csharp,
                                data.protoExepath);
                            break;
                        case 1:
                            ProtobufTool.Generate(data.outputFilePath, data.protoFilePath, ProtobufTool.java,
                                data.protoExepath);
                            break;
                        case 2:
                            ProtobufTool.Generate(data.outputFilePath, data.protoFilePath, ProtobufTool.cpp,
                                data.protoExepath);
                            break;
                    }
            }

            ;
            height += 100;

            GUI.Label(new Rect(0, height, 150, 30), "消息文件XML位置");
            data.messageXMLFilePath = GUI.TextField(new Rect(150, height, 400, 30), data.messageXMLFilePath);
            if (GUI.Button(new Rect(560, height, 100, 30), "点击选择文件"))
            {
                data.messageXMLFilePath = EditorUtility.OpenFilePanel("消息文件XML位置", data.messageXMLFilePath, "xml");
            }

            height += 35;

            if (Event.current.type == EventType.DragExited && new Rect(0, height, 150, 30).Contains(Event.current.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                UnityEngine.Object[] objs = DragAndDrop.objectReferences;
                data.messageXMLFilePath = Directory.GetCurrentDirectory() + "\\" + AssetDatabase.GetAssetPath(objs[0]);
            }


            if (GUI.Button(new Rect(150, height, 400, 30), "生成 消息类"))
            {
                if (data.messageXMLFilePath != string.Empty)
                {
                    ProtocolTool.GenerateCSharpMessage(data.messageXMLFilePath);
                }
            }

            height += 35;

            if (GUI.Button(new Rect(150, height, 400, 30), "生成 消息池"))
            {
                if (data.messageXMLFilePath != string.Empty)
                {
                    ProtocolTool.GenerateCSharpMessagePool(data.messageXMLFilePath);
                }
            }
        }
    }
}