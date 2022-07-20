﻿using UnityEngine;
using System.Collections;
using com;

public class JumpingRaceSystem : MonoBehaviour
{
    public static JumpingRaceSystem instance;
    public GameObject block;

    public ChatPrototype chatRace1;
    public ChatPrototype chatRace2;
    public ChatPrototype chatRace3;
    public ChatPrototype chatRaceEnd;

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

    int _raceIndex;
    public CameraFollowWithLerp mainCamera;
    public ClickToMove move;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _raceIndex = 0;
    }

    public void WalkToCenter()
    {
        geralt.GoTo(moveToCenter_g.position);
        ezio.GoTo(moveToCenter_e.position);
    }

    public void StartRace()
    {
        if (_raceIndex == 0)
        {
            SetupRace(race1);
        }
        else if (_raceIndex == 1)
        {
            SetupRace(race2);
        }
        else if (_raceIndex == 2)
        {
            SetupRace(race3);
        }
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
            InventoryBehaviour.instance.Hide();
            mainCamera.enabled = false;
            move.ForceStop(true);
            move.characterAnimation.Happy();
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
        e4.TimeToNext = 3.0f;
        e4.trans = race.cam_2;
        e4.duration = 2.5f;
        e4.type = CinematicActionTypes.TweenPositionAndRotation;
        e4.ease = DG.Tweening.Ease.InCubic;

        CinematicEventPrototype e5 = new CinematicEventPrototype();
        e5.TimeToNext = 0f;
        e5.trans = race.cam_3;
        e5.type = CinematicActionTypes.SetPositionAndRotation;

        CinematicEventPrototype e6 = new CinematicEventPrototype();
        e6.TimeToNext = 0f;
        e6.type = CinematicActionTypes.CallFunc;
        e6.action = () =>
        {
            ezio.transform.position = race.end_e_1.position;
            geralt.transform.position = race.end_g_1.position;
            ezio.FallTo(race.end_e_2.position, true);
            geralt.FallTo(race.end_g_2.position, false);
        };

        CinematicEventPrototype e7 = new CinematicEventPrototype();
        e7.TimeToNext = 4.0f;
        e7.trans = race.cam_4;
        e7.duration = 3.0f;
        e7.type = CinematicActionTypes.TweenPositionAndRotation;
        e7.ease = DG.Tweening.Ease.InCubic;

        CinematicEventPrototype e8 = new CinematicEventPrototype();
        e8.TimeToNext = 0.5f;
        e8.type = CinematicActionTypes.CallFunc;
        e8.action = () =>
        {
            ezio.ResetMove();
            geralt.ResetMove();
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

    void EndRace()
    {
        _raceIndex++;
        if (_raceIndex > 2)
        {
            EndAllRaces();
        }
    }

    void EndAllRaces()
    {
        block.SetActive(false);
        ChatService.instance.ShowChat(chatRaceEnd);
    }
}
