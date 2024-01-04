using AE_ClientNet;
using NetGameRunning;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using AE_ClientServerNet;

public class ChatRoom : MonoBehaviour
{
    public Text words;
    public Button sendButton;  
    public InputField messgeInputField;
    public float timer = 0;
    public float emptyMessageSendInterval = 0.3f;

    public void Init()
    {
        words.text = "";

        StartCoroutine(SendEnter());

        sendButton.onClick.AddListener(() =>
        {
            NetAsyncMgr.Send(new ChatMessage() { data = new GlobalChatData() { ChatWords = messgeInputField.text } });
            messgeInputField.text = "";
        });
       
        NetAsyncMgr.AddNetMessageListener(MessagePool.ChatMessage_ID, (message) =>
        {
            words.text += "\r\n" + (message as ChatMessage).data.ChatWords;
        });

        NetAsyncMgr.AddNetMessageListener(MessagePool.EmptyMessage_ID, ReciveEmptyMessage);
    }

    private void ReciveEmptyMessage(BaseMessage obj)
    {
        Debug.Log("空消息");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= emptyMessageSendInterval)
        {
            // NetAsyncMgr.Send(
            //     new EmptyMessage()
            //     {
            //         data = new EmptyMessageData() { X = 10, Y = 100, Z = 500, Ex = 10, Ey = 100, Ez = 500, }
            //     }
            // );
            timer = 0;
        }

    }

    private void OnDestroy()
    {
        ServerProgram.Stop();
    }

    IEnumerator SendEnter()
    {
        yield return new WaitForSeconds(2);
        NetAsyncMgr.Send(new ChatMessage() { data = new GlobalChatData() { ChatWords = "加入服务器" } });
    }
}
