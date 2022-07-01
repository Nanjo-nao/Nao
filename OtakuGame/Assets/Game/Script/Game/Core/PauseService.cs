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
        GameTime.timeScale = 0;
    }

    public void Resume()
    {
        GameTime.timeScale = 1;
    }
}