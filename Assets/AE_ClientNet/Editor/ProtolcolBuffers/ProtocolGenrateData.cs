using UnityEngine;

namespace AE_ClientNet
{
    [CreateAssetMenu(fileName = "ProtocolGenrateData",menuName = "Protocal/ProtocolGenrateData")]
    public class ProtocolGenrateData : ScriptableObject
    {
        public string protoFilePath;
        public string messageXMLFilePath;
        public string outputFilePath;
        public string protoExepath;
    }
}