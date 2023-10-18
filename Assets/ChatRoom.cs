using AE_NetWork;
using NetGameRunning;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChatRoom : MonoBehaviour
{
    public Text words;
    public Text myLocalPoint;
    public Button sendButton;
    public InputField messgeInputField;

    public void Init()
    {
        words.text = "";

        StartCoroutine(SendEnter());

        myLocalPoint.text = "ME:" + "[" + "]";

        sendButton.onClick.AddListener(() =>
        {
            NetAsyncMgr.Instance.Send(new ChatMessage() { data = new GlobalChatData() { ChatWords = messgeInputField.text } });
        });

        messgeInputField.onEndEdit.AddListener((obj) =>
        {
            NetAsyncMgr.Instance.Send(new ChatMessage() { data = new GlobalChatData() { ChatWords = obj } });
        });


        NetAsyncMgr.AddListener(MessagePool.ChatMessage_ID, (message) =>
        {
            words.text += "\r\n" + (message as ChatMessage).data.ChatWords;
        });
    }

    IEnumerator SendEnter()
    {
        yield return new WaitForSeconds(1);
        NetAsyncMgr.Instance.Send(new ChatMessage() { data = new GlobalChatData() { ChatWords = "加入服务器" } });
    }
}
