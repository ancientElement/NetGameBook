using UnityEngine;

using AE_NetWork;

public class TesNetMgrAsync : MonoBehaviour
{
    public float ConnectIntervalTime = 1f;
    public float ReConnectTime = 0;

    public ChatRoom ChatRoom;

    private void Update()
    {
        if (!NetAsyncMgr.Instance.IsConnected)
        {
            ReConnectTime -= Time.deltaTime;
            if (ReConnectTime <= 0)
            {
                NetAsyncMgr.Instance.Connect("127.0.0.1", 8080);
                ReConnectTime = ConnectIntervalTime;
            }
        }
    }

    private void Start()
    {
        if (NetAsyncMgr.Instance == null)
        {
            var obj = new GameObject("NetAsyncMgr");
            obj.AddComponent<NetAsyncMgr>();
        }
        NetAsyncMgr.Instance.Connect("127.0.0.1", 8080);

        ChatRoom.Init();
    }
}
