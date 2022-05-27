using UnityEngine;
using com;

public class PauseService : MonoBehaviour
{
    public static PauseService instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}