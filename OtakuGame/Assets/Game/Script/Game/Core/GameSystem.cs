using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance { get; private set; }

    public GameObject kabi;
    public GameObject kabiObs;
    public ParticleSystem kabiObsDisappear;
    public ParticleSystem kabiDisappear;

    public GameObject kabiChat1;
    public GameObject kabiChat2;
    public GameObject kabiChat3;

    public int kabiStep = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetKabiStep1();
    }

    public void SetKabiStep1()
    {
        kabiStep = 0;
        SyncKabiChat();
    }

    public void SetKabiStep2()
    {
        kabiStep = 1;
        SyncKabiChat();
    }

    public void SetKabiStep3()
    {
        kabiStep = 2;
        SyncKabiChat();
    }

    public void SetKabiStep4()
    {
        kabiStep = 3;
        SyncKabiChat();
    }

    public void RemoveObstacle()
    {
        kabiObsDisappear.Play();
        Destroy(kabiObs);
    }

    public void RemoveKabi()
    {
        kabiDisappear.Play();
        Destroy(kabi);
    }

    void SyncKabiChat()
    {
        kabiChat1.SetActive(kabiStep == 0);
        kabiChat2.SetActive(kabiStep == 1);
        kabiChat3.SetActive(kabiStep == 2);
    }
}
