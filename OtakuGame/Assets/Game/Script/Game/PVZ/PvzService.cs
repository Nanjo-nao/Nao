using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com;

public class PvzService : MonoBehaviour
{
    public static PvzService instance { get; private set; }
    public CameraFollow mainCamera;
    public ClickToMove move;
    public Transform transParam1;
    public Transform transParam2;
    public Transform transParam3;

    public List<ZombieBehaviour> zombies = new List<ZombieBehaviour>();
    public List<PlantBehaviour> plants = new List<PlantBehaviour>();

    private void Awake()
    {
        instance = this;
    }

    public void InitiatePvzArena()
    {
        ClearZombie();
        ClearPlants();
    }

    void ClearZombie()
    {
        foreach (var i in zombies)
        {
            GameObject.Destroy(i.gameObject);
        }
        zombies = new List<ZombieBehaviour>();
    }

    void ClearPlants()
    {
        foreach (var i in plants)
        {
            GameObject.Destroy(i.gameObject);
        }
        plants = new List<PlantBehaviour>();
    }


    void OnZombieEnters()
    {
        //loose
    }

    void Loose()
    {

    }

    void Win()
    {

    }

    public void ExitPvzView()
    {
        ClearPvzItems();
        mainCamera.enabled = true;
        move.enabled = true;
    }

    public void EnterPvzView()
    {
        var cinematic = CinematicCameraService.instance;
        cinematic.ResetEvents();

        CinematicEventPrototype e1 = new CinematicEventPrototype();
        e1.TimeToNext = 0;
        e1.type = CinematicActionTypes.CallFunc;
        e1.action = () =>
        {
            InventoryBehaviour.instance.Hide();
            SetPvzItems();
            mainCamera.enabled = false;
            move.enabled = false;
        };

        CinematicEventPrototype e2 = new CinematicEventPrototype();
        e2.TimeToNext = 1.0f;
        e2.trans = transParam1;

        e2.duration = 1.0f;
        e2.ease = DG.Tweening.Ease.OutCubic;
        e2.type = CinematicActionTypes.TweenPositionAndRotation;

        CinematicEventPrototype e3 = new CinematicEventPrototype();
        e3.TimeToNext = 5;
        e3.duration = 5f;
        e3.ease = DG.Tweening.Ease.InOutCubic;
        e3.type = CinematicActionTypes.TweenPositionAndRotation;
        e3.trans = transParam2;

        CinematicEventPrototype e4 = new CinematicEventPrototype();
        e4.TimeToNext = 2.0f;
        e4.trans = transParam3;
        e4.duration = 2.0f;
        e4.type = CinematicActionTypes.TweenPositionAndRotation;
        e4.ease = DG.Tweening.Ease.InOutCubic;

        CinematicEventPrototype e5 = new CinematicEventPrototype();
        e5.TimeToNext = 0;
        e5.type = CinematicActionTypes.CallFunc;
        e5.action = () =>
        {
            InventoryBehaviour.instance.Show();
            InitiatePvzArena();
        };

        cinematic.AddEvents(e1);
        cinematic.AddEvents(e2);
        cinematic.AddEvents(e3);
        cinematic.AddEvents(e4);
        cinematic.AddEvents(e5);

        cinematic.StartService();
        ClearPvzItems();
        GivePvzItems();
    }

    public void SetPvzItems()
    {
        ClearPvzItems();
        GivePvzItems();
    }

    void ClearPvzItems()
    {
        InventoryService.instance.RemoveItem("fsht", 100);
        InventoryService.instance.RemoveItem("psht", 100);
    }

    void GivePvzItems()
    {
        InventoryService.instance.AddItem("fsht", 2);
        InventoryService.instance.AddItem("psht", 5);
    }
}
