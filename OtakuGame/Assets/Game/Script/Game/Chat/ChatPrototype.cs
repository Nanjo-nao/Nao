using UnityEngine;
using com;
using System.Collections.Generic;

[CreateAssetMenu]
public class ChatPrototype : ScriptableObject
{
    public string content;
    public bool isRight;
    public string soundName;
    public ChatPrototype next;
    public enum ChatSprite
    {
        Kabi,
        Melina,
        Garrot,
    }
    public ChatSpecialAction chatSpecialAction;

    public enum ChatSpecialAction
    {
        None,

    }
    public ChatSprite chatSprite;

    public List<ChatButtonData> chatButtonDatas;

    public Sprite sprite
    {
        get
        {
            var cfg = ConfigService.instance.chatConfig;
            switch (chatSprite)
            {

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
        }

        public ButtonActionType buttonActionType;
    }
}