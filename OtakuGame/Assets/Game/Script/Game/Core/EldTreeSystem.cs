using UnityEngine;
using com;

public class EldTreeSystem : MonoBehaviour
{
    public static EldTreeSystem instance;
    public CommonNpcBehaviour melina;

    public GameObject ThreeFinger;
    public GameObject ThreeFingerSpawner;
    public Transform melina_pos_1;
    public Transform melina_pos_2;
    public Transform three_pos_1;
    public Transform three_pos_2;

    public CameraFollowWithLerp mainCamera;
    public ClickToMove move;

    public ChatPrototype melinaChat;
    public ChatPrototype fingerChat;
    public ChatPrototype burnChat;

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
        cinematic.AddEvents(e5);
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
        e4.TimeToNext = 7.0f;
        e4.duration = 5.0f;
        e4.trans = three_pos_2;
        e4.type = CinematicActionTypes.TweenPositionAndRotation;
        e4.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e5 = new CinematicEventPrototype();
        e5.TimeToNext = 2.2f;
        e5.action = () =>
        {
            ThreeFinger.SetActive(true);
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

    }
}
