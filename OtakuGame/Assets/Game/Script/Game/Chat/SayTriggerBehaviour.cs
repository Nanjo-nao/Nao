using UnityEngine;
using System.Collections.Generic;

public class SayTriggerBehaviour : MonoBehaviour
{
    public ChatPrototype chat;

    private int _triggeredCount;
    public int maxTriggerCount = 1;
    public float delay = 0;

    void ShowChat()
    {
        ChatService.instance.ShowChat(chat);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggeredCount >= maxTriggerCount)
            return;

        _triggeredCount++;
        if (delay > 0)
            Invoke("ShowChat", delay);
        else
            ShowChat();
    }
}