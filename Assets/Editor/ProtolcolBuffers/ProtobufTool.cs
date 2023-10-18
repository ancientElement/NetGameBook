using System.IO;
using System.Diagnostics;
using UnityEditor;

namespace ProtocolGenerateTool
{
    public class ProtobufTool
    {
        public static string csharp = "--csharp_out";
        public static string cpp = "--cpp_out";
        public static string java = "--java_out";


        public static void Generate(string outPath, string protoDirpath, string outCommond,string protoExepath)
        {
            DirectoryInfo directory = Directory.CreateDirectory(protoDirpath);

            FileInfo[] files = directory.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension == ".proto")
                {
                    Process cmd = new Process();

                    if (File.Exists(protoExepath))
                    {
                        cmd.StartInfo.FileName = protoExepath;

                        cmd.StartInfo.Arguments = $"-I={protoDirpath} {outCommond}={outPath} {files[i].Name}";
                        cmd.Start();
                        UnityEngine.Debug.Log(files[i].Name);
                    }
                    else
                    {
                        UnityEngine.Debug.Log("缺少protoc.exe文件");
                    }
                }
            }

            AssetDatabase.Refresh();
        }
    }
}