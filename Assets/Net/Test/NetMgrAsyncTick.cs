using UnityEngine;

using AE_NetWork;

public class NetMgrAsyncTick : MonoBehaviour
{
    public float ConnectIntervalTime = 1f;
    public float ReConnectTime = 0;

    public ChatRoom ChatRoom;

    private void Update()
    {
        if (!NetAsyncMgr.IsConnected)
        {
            ReConnectTime += Time.deltaTime;
            if (ReConnectTime >= ConnectIntervalTime)
            {
                NetAsyncMgr.Connect("127.0.0.1", 8080);
                ReConnectTime = 0;
            }
        }
        else
        {
            NetAsyncMgr.Update();
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);

        NetAsyncMgr.Connect("127.0.0.1", 8080);

        ChatRoom.Init();
    }
}
