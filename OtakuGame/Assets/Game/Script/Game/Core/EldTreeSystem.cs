using UnityEngine;
using com;
using DG.Tweening;

public class EldTreeSystem : MonoBehaviour
{
    public static EldTreeSystem instance;
    public CommonNpcBehaviour melina;

    public GameObject ThreeFinger;
    public GameObject ThreeFingerExtraFire;
    public GameObject ThreeFingerSpawner;
    public Transform melina_pos_1;
    public Transform melina_pos_2;
    public Transform three_pos_1;
    public Transform three_pos_2;
    public Transform threeFingerTalk_pos_1;
    public Transform threeFingerTalk_pos_2;
    public Transform tree_pos_1;
    public Transform tree_pos_2;
    public Transform tree_pos_3;
    public CameraFollowWithLerp mainCamera;
    public ClickToMove move;

    public ChatPrototype melinaChat;
    public ChatPrototype fingerChat;
    public ChatPrototype burnChat;
    public GameObject burn1;
    public GameObject burn2;
    bool _treeFingerTalked;
    public GameObject meetAgainTrigger;
    public CanvasGroup cgEnd;
    private void Awake()
    {
        instance = this;
    }

    public void Knee()
    {
        var cinematic = CinematicCameraService.instance;
        cinematic.ResetEvents();

        CinematicEventPrototype e1 = new CinematicEventPrototype();
        e1.TimeToNext = 0;
        e1.type = CinematicActionTypes.CallFunc;
        e1.action = () =>
        {
            //InventoryBehaviour.instance.Hide();
            mainCamera.SetEnable(false);
            move.ForceStop(true);
        };

        CinematicEventPrototype e2 = new CinematicEventPrototype();
        e2.TimeToNext = 2f;
        e2.trans = melina_pos_1;
        e2.type = CinematicActionTypes.TweenPositionAndRotation;
        e2.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e3 = new CinematicEventPrototype();
        e3.TimeToNext = 0f;
        e3.type = CinematicActionTypes.CallFunc;
        e3.action = () =>
        {
            melina.Knee();
        };

        CinematicEventPrototype e4 = new CinematicEventPrototype();
        e4.TimeToNext = 4.0f;
        e4.trans = melina_pos_2;
        e4.type = CinematicActionTypes.TweenPositionAndRotation;
        e4.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e5 = new CinematicEventPrototype();
        e5.TimeToNext = 0f;
        e5.action = () =>
        {
            ChatService.instance.ShowChat(melinaChat);
        };

        e5.type = CinematicActionTypes.CallFunc;

        cinematic.AddEvents(e1);
        cinematic.AddEvents(e2);
        cinematic.AddEvents(e3);
        cinematic.AddEvents(e4);
        cinematic.AddEvents(e5);
        cinematic.StartService();
    }

    public void ShowTreeFinger()
    {
        var cinematic = CinematicCameraService.instance;
        cinematic.ResetEvents();

        CinematicEventPrototype e1 = new CinematicEventPrototype();
        e1.TimeToNext = 0;
        e1.type = CinematicActionTypes.CallFunc;
        e1.action = () =>
        {
            //InventoryBehaviour.instance.Hide();
            mainCamera.SetEnable(false);
            move.ForceStop(true);
        };

        CinematicEventPrototype e2 = new CinematicEventPrototype();
        e2.TimeToNext = 2f;
        e2.trans = three_pos_1;
        e2.type = CinematicActionTypes.TweenPositionAndRotation;
        e2.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e3 = new CinematicEventPrototype();
        e3.TimeToNext = 0f;
        e3.type = CinematicActionTypes.CallFunc;
        e3.action = () =>
        {
            ThreeFingerSpawner.SetActive(true);
        };

        CinematicEventPrototype e4 = new CinematicEventPrototype();
        e4.TimeToNext = 6.5f;
        e4.duration = 5.0f;
        e4.trans = three_pos_2;
        e4.type = CinematicActionTypes.TweenPositionAndRotation;
        e4.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e5 = new CinematicEventPrototype();
        e5.TimeToNext = 2.5f;
        e5.action = () =>
        {
            ThreeFinger.SetActive(true);
            com.SoundService.instance.Play("showFinger");
        };
        e5.type = CinematicActionTypes.CallFunc;

        CinematicEventPrototype e6 = new CinematicEventPrototype();
        e6.TimeToNext = 0.0f;
        e6.type = CinematicActionTypes.CallFunc;
        e6.action = () =>
        {
            mainCamera.SetEnable(true);
            move.ForceStop(false);
        };

        cinematic.AddEvents(e1);
        cinematic.AddEvents(e2);
        cinematic.AddEvents(e3);
        cinematic.AddEvents(e4);
        cinematic.AddEvents(e5);
        cinematic.AddEvents(e6);
        cinematic.StartService();
    }

    public void BurnTree()
    {
        Debug.Log("BurnTree");
        var cinematic = CinematicCameraService.instance;
        cinematic.ResetEvents();

        com.SoundService.instance.Play("burnTree");

        CinematicEventPrototype e1 = new CinematicEventPrototype();
        e1.TimeToNext = 0;
        e1.type = CinematicActionTypes.CallFunc;
        e1.action = () =>
        {
            //InventoryBehaviour.instance.Hide();
            mainCamera.SetEnable(false);
            move.ForceStop(true);
        };

        CinematicEventPrototype e2 = new CinematicEventPrototype();
        e2.TimeToNext = 2f;
        e2.trans = tree_pos_1;
        e2.type = CinematicActionTypes.TweenPositionAndRotation;
        e2.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e3 = new CinematicEventPrototype();
        e3.TimeToNext = 0f;
        e3.type = CinematicActionTypes.CallFunc;
        e3.action = () =>
        {
            burn1.SetActive(true);
            RenderSettings.fogColor = new Color(0.88f, 0.74f, 0.62f);
            //AFB999
        };

        CinematicEventPrototype e4 = new CinematicEventPrototype();
        e4.TimeToNext = 4f;
        e4.duration = 4f;
        e4.trans = tree_pos_2;
        e4.type = CinematicActionTypes.TweenPositionAndRotation;
        e4.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e5 = new CinematicEventPrototype();
        e5.TimeToNext = 0.25f;
        e5.action = () =>
        {
            RenderSettings.fogColor = new Color(0.9f, 0.77f, 0.6f);
        };
        e5.type = CinematicActionTypes.CallFunc;

        CinematicEventPrototype e6 = new CinematicEventPrototype();
        e6.TimeToNext = 0.25f;
        e6.type = CinematicActionTypes.CallFunc;
        e6.action = () =>
        {
       
            RenderSettings.fogColor = new Color(0.95f, 0.78f, 0.56f);
        };

        CinematicEventPrototype e7 = new CinematicEventPrototype();
        e7.TimeToNext = 3f;
        e7.trans = tree_pos_3;
        e7.type = CinematicActionTypes.TweenPositionAndRotation;
        e7.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e8 = new CinematicEventPrototype();
        e8.TimeToNext = 0.0f;
        e8.type = CinematicActionTypes.CallFunc;
        e8.action = () =>
        {
            //burn2.SetActive(true);
            burn2.SetActive(true);
            RenderSettings.fogColor = new Color(1f, 0.79f, 0.55f);
            cgEnd.blocksRaycasts = true;
            cgEnd.interactable = true;
            cgEnd.DOFade(1, 5).SetEase(Ease.InOutCubic).SetDelay(3f);
        };

        cinematic.AddEvents(e1);
        cinematic.AddEvents(e2);
        cinematic.AddEvents(e3);
        cinematic.AddEvents(e4);
        cinematic.AddEvents(e5);
        cinematic.AddEvents(e6);
        cinematic.AddEvents(e7);
        cinematic.AddEvents(e8);
        cinematic.StartService();
    }

    public void SeeTreeFinger()
    {
        if (_treeFingerTalked)
            return;

        _treeFingerTalked = true;

        var cinematic = CinematicCameraService.instance;
        cinematic.ResetEvents();

        CinematicEventPrototype e1 = new CinematicEventPrototype();
        e1.TimeToNext = 0;
        e1.type = CinematicActionTypes.CallFunc;
        e1.action = () =>
        {
            //InventoryBehaviour.instance.Hide();
            mainCamera.SetEnable(false);
            move.ForceStop(true);
        };

        CinematicEventPrototype e2 = new CinematicEventPrototype();
        e2.TimeToNext = 3.0f;
        e2.trans = threeFingerTalk_pos_1;
        e2.type = CinematicActionTypes.TweenPositionAndRotation;
        e2.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e3 = new CinematicEventPrototype();
        e3.TimeToNext = 0f;
        e3.type = CinematicActionTypes.CallFunc;
        e3.action = () =>
        {
            ThreeFingerExtraFire.SetActive(true);
            melina.Unknee();
        };

        CinematicEventPrototype e4 = new CinematicEventPrototype();
        e4.TimeToNext = 2.0f;
        e4.trans = threeFingerTalk_pos_2;
        e4.type = CinematicActionTypes.TweenPositionAndRotation;
        e4.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e5 = new CinematicEventPrototype();
        e5.TimeToNext = 0f;
        e5.action = () =>
        {
            InventoryBehaviour.instance.Show();
            ChatService.instance.ShowChat(fingerChat);
        };

        e5.type = CinematicActionTypes.CallFunc;

        cinematic.AddEvents(e1);
        cinematic.AddEvents(e2);
        cinematic.AddEvents(e3);
        cinematic.AddEvents(e4);
        cinematic.AddEvents(e5);
        cinematic.StartService();
    }

    public void EndFingerChat()
    {
        mainCamera.SetEnable(true);
        move.ForceStop(false);
        meetAgainTrigger.SetActive(true);
    }

    public void MeetAgain()
    {
        var cinematic = CinematicCameraService.instance;
        cinematic.ResetEvents();

        CinematicEventPrototype e1 = new CinematicEventPrototype();
        e1.TimeToNext = 0;
        e1.type = CinematicActionTypes.CallFunc;
        e1.action = () =>
        {
            //InventoryBehaviour.instance.Hide();
            mainCamera.SetEnable(false);
            move.ForceStop(true);
            InventoryBehaviour.instance.Hide();
        };

        CinematicEventPrototype e2 = new CinematicEventPrototype();
        e2.TimeToNext = 2f;
        e2.trans = melina_pos_1;
        e2.type = CinematicActionTypes.TweenPositionAndRotation;
        e2.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e3 = new CinematicEventPrototype();
        e3.TimeToNext = 0f;
        e3.type = CinematicActionTypes.CallFunc;
        e3.action = () =>
        {
            melina.Knee();
        };

        CinematicEventPrototype e4 = new CinematicEventPrototype();
        e4.TimeToNext = 4.0f;
        e4.trans = melina_pos_2;
        e4.type = CinematicActionTypes.TweenPositionAndRotation;
        e4.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e5 = new CinematicEventPrototype();
        e5.TimeToNext = 0f;
        e5.action = () =>
        {
            ChatService.instance.ShowChat(burnChat);
        };

        e5.type = CinematicActionTypes.CallFunc;

        cinematic.AddEvents(e1);
        cinematic.AddEvents(e2);
        cinematic.AddEvents(e3);
        cinematic.AddEvents(e4);
        cinematic.AddEvents(e5);
        cinematic.StartService();
    }
}
