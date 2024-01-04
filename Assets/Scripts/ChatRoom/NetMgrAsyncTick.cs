using UnityEngine;
using AE_ClientNet;
using UnityEngine.UI;
using System.Net;
using AE_ClientServerNet;

public class NetMgrAsyncTick : MonoBehaviour
{
    public float ConnectIntervalTime = 1f;
    public float ReConnectTime = 0;
    public bool startConnect;

    public Button connectButton, startServerButton;
    public InputField serverIPAndPortInputFiled, openServerIPInputFiled;
    public Text openServerIPAndPointText;

    private string serverIPAndPoint = "127.0.0.1:8080";
    public string serverIP => serverIPAndPoint.Split(":")[0];
    public int serverPort => int.Parse(serverIPAndPoint.Split(":")[1]);

    private string openServerIP = "127.0.0.1";
    private int openServerPoint = 8080;

    public ChatRoom ChatRoom;

    private void Start()
    {
        DontDestroyOnLoad(this);

        serverIPAndPortInputFiled.onValueChanged.AddListener((value) =>
        {
            serverIPAndPoint = value;
        });

        openServerIPInputFiled.onValueChanged.AddListener((value) =>
        {
            openServerPoint = int.Parse(value);
        });

        connectButton.onClick.AddListener(() =>
        {
            if (!startConnect)
            {
                startConnect = true;
                NetAsyncMgr.Connect(serverIP, serverPort);
            }
        });

        startServerButton.onClick.AddListener(() =>
        {
            if (ServerProgram.Started) return;

            openServerIP = GetLocalIPAddress();
            openServerIPAndPointText.text = openServerIP + ":" + openServerPoint;
            serverIPAndPoint = openServerIP + ":" + openServerPoint;
            serverIPAndPortInputFiled.text = serverIPAndPoint;
            ServerProgram.Start(openServerIP, openServerPoint);

            startConnect = true;
            NetAsyncMgr.Connect(serverIP, serverPort);
        });

        ChatRoom.Init();
    }

    private void Update()
    {
        if (startConnect)
        {
            if (!NetAsyncMgr.IsConnected)
            {
                ReConnectTime += Time.deltaTime;
                if (ReConnectTime >= ConnectIntervalTime)
                {
                    NetAsyncMgr.Connect(serverIP, serverPort);
                    ReConnectTime = 0;
                }
            }
            else
            {
                NetAsyncMgr.Update();
            }
        }
    }

    string GetLocalIPAddress()
    {
        string localIP = string.Empty;
        string hostName = Dns.GetHostName();
        IPHostEntry host = Dns.GetHostEntry(hostName);

        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }

        return localIP;
    }
}
