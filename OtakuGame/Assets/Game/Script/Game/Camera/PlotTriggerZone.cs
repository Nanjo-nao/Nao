using UnityEngine;
using com;

public class PlotTriggerZone : MonoBehaviour
{
    private bool _triggered;
    public string tagChecker;

    public Transform transParam1;
    public Transform transParam2;
    public Transform transParam3;

    public enum PlotTriggerCheckMethods
    {
        Tag,
    }

    public PlotTriggerCheckMethods checkMethod;

    public enum PlotTriggerEvents
    {
        Mission1_spawnBombers
    }

    public PlotTriggerEvents plotTriggerEvent;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (_triggered)
            return;

        var toCheck = false;
        switch (checkMethod)
        {
            case PlotTriggerCheckMethods.Tag:
                toCheck = (other.gameObject.tag == tagChecker);
                break;
        }

        if (toCheck) Trigger();
    }

    void Trigger()
    {
        Debug.Log("PlotTriggerZone " + gameObject.name + " Trigger");

        _triggered = true;
        var cinematic = CinematicCameraService.instance;

        switch (plotTriggerEvent)
        {
            case PlotTriggerEvents.Mission1_spawnBombers:
                cinematic.ResetEvents();

                CinematicEventPrototype e1 = new CinematicEventPrototype();
                e1.TimeToNext = 0;
                e1.type = CinematicActionTypes.CallFunc;
                e1.action = () =>
                {
                    //SpawnService.instance.PausePlayerCam();
                   // SpawnService.instance.PausePlayerPlane();
                };

                CinematicEventPrototype e2 = new CinematicEventPrototype();
                e2.TimeToNext = 1.0f;
                e2.trans = transParam1;

                e2.duration = 1.0f;
                e2.ease = DG.Tweening.Ease.OutCubic;
                e2.type = CinematicActionTypes.TweenPositionAndRotation;
                //e2.type = CinematicActionTypes.SetPositionAndRotation;

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
                    //SpawnService.instance.ResumePlayerCam();
                   // SpawnService.instance.ResumePlayerPlane();
                };

                cinematic.AddEvents(e1);
                cinematic.AddEvents(e2);
                cinematic.AddEvents(e3);
                cinematic.AddEvents(e4);
                cinematic.AddEvents(e5);

                cinematic.StartService();
                break;

        }
    }
}