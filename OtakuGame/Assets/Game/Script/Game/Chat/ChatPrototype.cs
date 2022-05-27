using UnityEngine;
using com;

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
}