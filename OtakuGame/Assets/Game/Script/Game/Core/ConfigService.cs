using UnityEngine;

public class ConfigService : MonoBehaviour
{
    public static ConfigService instance { get; private set; }

    public ChatConfig chatConfig;
    public ItemsConfig itemsConfig;

    private void Awake()
    {
        instance = this;
    }
}