using UnityEngine;

public class ChatService : MonoBehaviour
{
    public static ChatService instance { get; private set; }

    private ChatPrototype _chat;

    private void Awake()
    {
        instance = this;
    }

    public void ShowChat(ChatPrototype chat)
    {
        PauseService.instance.Pause();
        _chat = chat;
        ChatPanelBehaviour.instance.Show(_chat);
    }

    public void EndChat()
    {
        if (ChatPanelBehaviour.instance.TryShowButtons())
        {
            return;
        }

        PauseService.instance.Resume();

        ChatPanelBehaviour.instance.Hide();

        switch (_chat.chatSpecialAction)
        {
            case ChatPrototype.ChatSpecialAction.None:
                break;
        }

        _chat = null;
    }

    public void OnChatEnd()
    {
        if (_chat == null)
            return;

        if (_chat.next == null)
        {
            EndChat();
            return;
        }

        ShowChat(_chat.next);
    }

    public System.Action GetActionByButtonActionType(ChatPrototype.ChatButtonData.ButtonActionType type, ChatPrototype chatParam)
    {
        switch (type)
        {
            case ChatPrototype.ChatButtonData.ButtonActionType.None:
                break;

            case ChatPrototype.ChatButtonData.ButtonActionType.ÉÕÊ÷:
                return () => { Debug.Log("ÉÕÊ÷!!"); };

            case ChatPrototype.ChatButtonData.ButtonActionType.Chat:
                return () => { ChatService.instance.ShowChat(chatParam); };
        }

        return null;
    }
}