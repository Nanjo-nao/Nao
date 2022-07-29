using UnityEngine;
using com;

public class JumpingRaceSystem : MonoBehaviour
{
    public static JumpingRaceSystem instance;
    public GameObject block;

    public Transform moveToCenter_g;
    public Transform moveToCenter_e;

    public CommonNpcBehaviour geralt;
    public CommonNpcBehaviour ezio;

    [System.Serializable]
    public class RaceParam
    {
        public Transform start_g_1;
        public Transform start_e_1;
        public Transform start_g_2;
        public Transform start_e_2;
        public Transform end_g_1;
        public Transform end_e_1;
        public Transform end_g_2;
        public Transform end_e_2;
        public Transform cam_1;//cut to
        public Transform cam_2;//tween
        public Transform cam_3;//cut to
        public Transform cam_4;//tween
    }

    public RaceParam race1;
    public RaceParam race2;
    public RaceParam race3;

    public ChatPrototype winChat;
    public ChatPrototype looseChat;
    public ChatPrototype betChat;
    public ChatPrototype continueChat;
    public ChatPrototype endingChat;

    bool _receiveInput;

    int raceIndex
    {
        get
        {
            var c = InventoryService.instance.GetItemCount("coin");
            return c;
        }
    }

    public CameraFollowWithLerp mainCamera;
    public ClickToMove move;

    bool _betEzioWin;

    private void Awake()
    {
        instance = this;
        _receiveInput = false;
    }

    public void WalkToCenter()
    {
        geralt.GoTo(moveToCenter_g.position);
        ezio.GoTo(moveToCenter_e.position);
        InventoryBehaviour.instance.Show();
        InventoryService.instance.RemoveItem("coin", 100);
    }

    ChatPrototype GetRaceEndChat()
    {
        var win = GetWinResult();
        return win ? winChat : looseChat;
    }

    bool GetWinResult()
    {
        return _betEzioWin;
    }

    public void OnBetEzio()
    {
        Debug.Log("OnBetEzio");
        if (!_receiveInput)
            return;

        _receiveInput = false;
        _betEzioWin = true;
        ContinueRace(currentRace);
    }

    public void OnBetGeraltWin()
    {
        Debug.Log("OnBetGeraltWin");
        if (!_receiveInput)
            return;

        _receiveInput = false;
        _betEzioWin = false;
        ContinueRace(currentRace);
    }

    RaceParam currentRace
    {
        get
        {
            if (raceIndex == 0)
                return race1;
            else if (raceIndex == 1)
                return race2;
            else if (raceIndex == 2)
                return race3;

            return null;
        }
    }

    public void TryStartRace()
    {
        if (currentRace == null)
        {
            EndAllRaces();
            return;
        }

        ChatService.instance.EndChat();
        ChatService.instance.ShowChat(continueChat);
    }

    public void StartRace()
    {
        _receiveInput = false;

        SetupRace(currentRace);
    }

    void SetupRace(RaceParam race)
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
        e2.TimeToNext = 0f;
        e2.trans = race.cam_1;
        e2.type = CinematicActionTypes.SetPositionAndRotation;

        CinematicEventPrototype e3 = new CinematicEventPrototype();
        e3.TimeToNext = 0f;
        e3.type = CinematicActionTypes.CallFunc;
        e3.action = () =>
        {
            ezio.transform.position = race.start_e_1.position;
            geralt.transform.position = race.start_g_1.position;
            ezio.GoTo(race.start_e_2.position);
            geralt.GoTo(race.start_g_2.position);
        };

        CinematicEventPrototype e4 = new CinematicEventPrototype();
        e4.TimeToNext = 4.0f;
        e4.trans = race.cam_2;
        e4.duration = 3.5f;
        e4.type = CinematicActionTypes.TweenPositionAndRotation;
        e4.ease = DG.Tweening.Ease.OutCubic;

        CinematicEventPrototype e5 = new CinematicEventPrototype();
        e5.TimeToNext = 0f;
        e5.action = () =>
        {
            //wait bet input
            ChatService.instance.ShowChat(betChat);
            _receiveInput = true;
        };

        e5.type = CinematicActionTypes.CallFunc;

        cinematic.AddEvents(e1);
        cinematic.AddEvents(e2);
        cinematic.AddEvents(e3);
        cinematic.AddEvents(e4);
        cinematic.AddEvents(e5);
        cinematic.StartService();
    }

    void ContinueRace(RaceParam race)
    {
        var cinematic = CinematicCameraService.instance;
        cinematic.ResetEvents();

        CinematicEventPrototype e1 = new CinematicEventPrototype();
        e1.TimeToNext = 1.5f;
        e1.duration = 1.5f;
        e1.trans = race.cam_3;
        e1.type = CinematicActionTypes.TweenPositionAndRotation;
        e1.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e2 = new CinematicEventPrototype();
        e2.TimeToNext = 0f;
        e2.type = CinematicActionTypes.CallFunc;
        e2.action = () =>
        {
            ezio.transform.position = race.end_e_1.position;
            geralt.transform.position = race.end_g_1.position;
            ezio.FallTo(race.end_e_2.position, true);
            geralt.FallTo(race.end_g_2.position, false);
        };

        CinematicEventPrototype e3 = new CinematicEventPrototype();
        e3.TimeToNext = 4.0f;
        e3.trans = race.cam_4;
        e3.duration = 2.5f;
        e3.type = CinematicActionTypes.TweenPositionAndRotation;
        e3.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e4 = new CinematicEventPrototype();
        e4.TimeToNext = 0.5f;
        e4.type = CinematicActionTypes.CallFunc;
        e4.action = () =>
        {
            ezio.ResetMove();
            geralt.ResetMove();
            mainCamera.SetEnable(true);
            geralt.transform.position = moveToCenter_g.position;
            ezio.transform.position = moveToCenter_e.position;
            geralt.Face(move.transform.position);
            ezio.Face(move.transform.position);
        };

        CinematicEventPrototype e5 = new CinematicEventPrototype();
        e5.TimeToNext = 3.8f;
        e5.type = CinematicActionTypes.CallFunc;
        e5.action = () =>
        {
            mainCamera.SetEnable(true);
            ezio.Happy();
            ChatService.instance.ShowChat(GetRaceEndChat());
            GiveRaceReward();
        };

        CinematicEventPrototype e6 = new CinematicEventPrototype();
        e6.TimeToNext = 0.5f;
        e6.type = CinematicActionTypes.CallFunc;
        e6.action = () =>
        {
            ezio.ResetMove();
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

    void GiveRaceReward()
    {
        Debug.Log("!!!GiveRaceReward");
        Debug.Log(GetWinResult());
        InventoryBehaviour.instance.SyncItems();
        if (GetWinResult())
            InventoryService.instance.AddItem("coin", 1);
        else
            InventoryService.instance.RemoveItem("coin", 1);
        InventoryBehaviour.instance.SyncItems();
    }

    void EndAllRaces()
    {
        block.SetActive(false);
        InventoryBehaviour.instance.Hide();
        mainCamera.SetEnable(true);
        move.ForceStop(false);
        ChatService.instance.ShowChat(endingChat);
    }
}