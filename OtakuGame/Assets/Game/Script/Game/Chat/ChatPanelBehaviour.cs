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

    public bool hasBtnShowing { get; private set; }

    private ChatPrototype _chatPrototype;

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

        HideButtons();
    }

    void HideButtons()
    {
        hasBtnShowing = false;

        btn1.SetActive(false);
        btn2.SetActive(false);
        btn3.SetActive(false);
    }

    void ClearBtnCb()
    {
        _btn1Cb = null;
        _btn2Cb = null;
        _btn3Cb = null;
    }

    public void Show(ChatPrototype chat)
    {
        _chatPrototype = chat;

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

        HideButtons();
        ClearBtnCb();
    }

    public void UserTapped()
    {
        if (_textAnimation.Finished)
        {
            if (!hasBtnShowing)
            {
                ChatService.instance.OnChatEnd();
            }
            return;
        }

        _textAnimation.Skip();
    }

    public void TryShowButtons()
    {
        HideButtons();
        if (_chatPrototype != null && _chatPrototype.chatButtonDatas != null && _chatPrototype.chatButtonDatas.Count > 0)
        {
            ShowButtons();
        }
    }

    void ShowButtons()
    {
        hasBtnShowing = true;

        var datas = _chatPrototype.chatButtonDatas;
        ChatPrototype.ChatButtonData data;
        if (datas.Count > 0)
        {
            hasBtnShowing = true;

            data = datas[0];
            if (data != null)
            {
                btn1Txt.text = data.buttonText;
                btn1.SetActive(true);
                _btn1Cb = ChatService.instance.GetActionByButtonActionType(data.buttonActionType, data.paramChat);
            }
        }

        if (datas.Count > 1)
        {
            data = datas[1];
            if (data != null)
            {
                btn2Txt.text = data.buttonText;
                btn2.SetActive(true);
                _btn2Cb = ChatService.instance.GetActionByButtonActionType(data.buttonActionType, data.paramChat);
            }
        }

        if (datas.Count > 2)
        {
            data = datas[2];
            if (data != null)
            {
                btn3Txt.text = data.buttonText;
                btn3.SetActive(true);
                _btn3Cb = ChatService.instance.GetActionByButtonActionType(data.buttonActionType, data.paramChat);
            }
        }
    }

    void ClickBtnFeedback()
    {
        ChatService.instance.OnChatEnd();
        SoundService.instance.Play("btn");
        Debug.Log("OnClickBtn!!");
    }

    public void OnClickBtn1()
    {
        //ChatService.instance.PerformChatSpecialAction();
        ClickBtnFeedback();
        _btn1Cb?.Invoke();
    }

    public void OnClickBtn2()
    {
        ClickBtnFeedback();
        _btn2Cb?.Invoke();
    }

    public void OnClickBtn3()
    {
        ClickBtnFeedback();
        _btn3Cb?.Invoke();
    }
}