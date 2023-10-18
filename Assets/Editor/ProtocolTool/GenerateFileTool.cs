using System.IO;
using UnityEditor;
using UnityEngine;

namespace ProtocolGenerateTool
{
    public class GenerateFileTool
    {
        public static void Generate(string fileDirectory, string fileName, string text)
        {
            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);
            File.WriteAllText($"{fileDirectory + fileName}", text);
        }
    }
}