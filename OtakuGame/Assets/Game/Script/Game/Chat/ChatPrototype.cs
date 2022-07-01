using UnityEngine;
using com;
using System.Collections.Generic;

[CreateAssetMenu]
public class ChatPrototype : ScriptableObject
{
    public string content;
    public string soundName;
    public ChatPrototype next;

    public enum Speaker
    {
        None,
        Kabi,
        Melina,
        Garrot,
        Garrot_L,
        Ezio,
        ZZQ,
    }

    public ChatSpecialAction chatSpecialAction;

    public enum ChatSpecialAction
    {
        None,
        StartPvz,
        Shake1,
        Shake2,
        Shake3,
        StartRace,
        GiveMoneyTo3Finger,
        BurnTree,
    }
    public Speaker speaker;

    public List<ChatButtonData> chatButtonDatas;

    public bool isRight
    {
        get
        {
            switch (speaker)
            {
                case Speaker.Garrot_L:
                    return false;

                case Speaker.ZZQ:
                    return false;
            }
            return true;
        }
    }

    public Sprite sprite
    {
        get
        {
            var cfg = ConfigService.instance.chatConfig;
            switch (speaker)
            {
                case Speaker.None:
                    return null;

                case Speaker.Melina:
                    return cfg.mln_r;

                case Speaker.Kabi:
                    return null;

                case Speaker.Garrot:
                    return cfg.grt_r;

                case Speaker.Garrot_L:
                    return cfg.grt_l;

                case Speaker.Ezio:
                    return cfg.ezo_r;

                case Speaker.ZZQ:
                    return cfg.zzq_l;
            }
            return null;
        }
    }

    [System.Serializable]
    public class ChatButtonData
    {
        public string buttonText;
        public enum ButtonActionType
        {
            None,
            ÉÕÊ÷,
            Chat,
        }

        public ChatPrototype paramChat;
        public ButtonActionType buttonActionType;
    }
}