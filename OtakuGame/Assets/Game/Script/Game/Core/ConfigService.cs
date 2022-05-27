using UnityEngine;

public class ConfigService : MonoBehaviour
{
    public static ConfigService instance { get; private set; }

    public ChatConfig chatConfig;

    private void Awake()
    {
        instance = this;
    }
}