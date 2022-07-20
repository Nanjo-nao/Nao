using UnityEngine;
using com;

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
        if (chat == null)
        {
            Debug.LogError("Error! no chat");
            return;
        }

        PauseService.instance.Pause();
        ClickToMove.instance.Stop();
        _chat = chat;
        ChatPanelBehaviour.instance.Show(_chat);
    }

    public void PerformChatSpecialAction()
    {
        //Debug.Log("PerformChatSpecialAction");
        Debug.Log(_chat.chatSpecialAction);
        switch (_chat.chatSpecialAction)
        {
            case ChatPrototype.ChatSpecialAction.None:
                break;

            case ChatPrototype.ChatSpecialAction.StartPvz:
                PvzService.instance.EnterPvzView();
                break;

            case ChatPrototype.ChatSpecialAction.RestartPvz:
                PvzService.instance.EndPvz();
                PvzService.instance.EnterPvzView();
                break;

            case ChatPrototype.ChatSpecialAction.EndPvz:
                PvzService.instance.EndPvz();
                PvzService.instance.ExitPvzView();
                break;

            case ChatPrototype.ChatSpecialAction.Shake1:
                CameraShake.instance.Shake();
                break;

            case ChatPrototype.ChatSpecialAction.Shake2:
                CameraShake.instance.Shake();
                break;

            case ChatPrototype.ChatSpecialAction.Shake3:
                CameraShake.instanceStrong.Shake();
                break;

            case ChatPrototype.ChatSpecialAction.RemoveObstacle:
                CameraShake.instanceStrong.Shake();
                GameSystem.instance.SetKabiStep4();
                GameSystem.instance.RemoveObstacle();
                break;

            case ChatPrototype.ChatSpecialAction.Kabi2:
                GameSystem.instance.SetKabiStep2();
                break;

            case ChatPrototype.ChatSpecialAction.Kabi3:
                GameSystem.instance.SetKabiStep3();
                break;

            case ChatPrototype.ChatSpecialAction.Move2Jumpers:
                JumpingRaceSystem.instance.WalkToCenter();
                break;

            case ChatPrototype.ChatSpecialAction.StartRace:
                JumpingRaceSystem.instance.StartRace();
                break;
        }
    }

    public void EndChat()
    {
        PauseService.instance.Resume();
        ChatPanelBehaviour.instance.Hide();

        _chat = null;
    }

    public void OnChatEnd()
    {
        if (_chat == null)
            return;

        PerformChatSpecialAction();

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
                return () => { Debug.Log("None!!"); };

            case ChatPrototype.ChatButtonData.ButtonActionType.ÉÕÊ÷:
                return () => { Debug.Log("ÉÕÊ÷!!"); };

            case ChatPrototype.ChatButtonData.ButtonActionType.Chat:
                return () => { ShowChat(chatParam); };
        }

        return null;
    }
}