using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
using com;
using DG.Tweening;
using System;

public class ChatPanelBehaviour : MonoBehaviour
{
    private Text _text;
    public Image right;
    public Image left;
    public CanvasGroup cg;
    private DialogTextAnimation _textAnimation;
    public static ChatPanelBehaviour instance { get; private set; }

    public GameObject btn1;
    public GameObject btn2;
    public GameObject btn3;
    public Text btn1Txt;
    public Text btn2Txt;
    public Text btn3Txt;
    private Action _btn1Cb;
    private Action _btn2Cb;
    private Action _btn3Cb;

    private void Awake()
    {
        instance = this;

        _text = GetComponentInChildren<Text>();
        _textAnimation = _text.GetComponent<DialogTextAnimation>();
        Hide();
    }

    public void Hide()
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
        _text.SetText("");

        ResetButtons();
    }

    void ResetButtons()
    {
        btn1.SetActive(false);
        btn2.SetActive(false);
        btn3.SetActive(false);

        _btn1Cb = null;
        _btn2Cb = null;
        _btn3Cb = null;
    }

    public void Show(ChatPrototype chat)
    {
        string sound = chat.soundName;
        if (string.IsNullOrEmpty(sound))
        {
            sound = "chat";
        }

        SoundService.instance.Play(sound);

        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;

        _text.DOKill();
        _text.SetText(chat.content);
        _text.ForceMeshUpdate();
        _textAnimation.StartAnimation();

        right.enabled = false;
        left.enabled = false;
        if (chat.sprite != null)
        {
            if (chat.isRight)
            {
                right.enabled = true;
                right.sprite = chat.sprite;
            }
            else
            {
                left.enabled = true;
                left.sprite = chat.sprite;
            }
        }

        ResetButtons();
        if (chat.chatButtonDatas != null)
        {
            ChatPrototype.ChatButtonData data;
            if (chat.chatButtonDatas.Count > 0)
            {
                data = chat.chatButtonDatas[0];
                if (data != null)
                {
                    btn1Txt.text = data.buttonText;
                    btn1.SetActive(true);
                    _btn1Cb = ChatService.instance.GetActionByButtonActionType(data.buttonActionType);
                }
            }

            if (chat.chatButtonDatas.Count > 1)
            {
                data = chat.chatButtonDatas[1];
                if (data != null)
                {
                    btn2Txt.text = data.buttonText;
                    btn2.SetActive(true);
                    _btn2Cb = ChatService.instance.GetActionByButtonActionType(data.buttonActionType);
                }
            }

            if (chat.chatButtonDatas.Count > 2)
            {
                data = chat.chatButtonDatas[2];
                if (data != null)
                {
                    btn3Txt.text = data.buttonText;
                    btn3.SetActive(true);
                    _btn3Cb = ChatService.instance.GetActionByButtonActionType(data.buttonActionType);
                }
            }
        }
    }

    public void UserTapped()
    {
        if (_textAnimation.Finished)
        {
            ChatService.instance.OnChatEnd();
        }
    }

    public void OnClickBtn1()
    {
        _btn1Cb?.Invoke();
        SoundService.instance.Play("btn");
    }

    public void OnClickBtn2()
    {
        _btn2Cb?.Invoke();
        SoundService.instance.Play("btn");
    }

    public void OnClickBtn3()
    {
        _btn3Cb?.Invoke();
        SoundService.instance.Play("btn");
    }
}