using com;
using UnityEngine;
using UnityEngine.EventSystems;


public class InputPanelService : MonoBehaviour, IGameFlow
{
    public static InputPanelService instance { get; private set; }
    public Camera MainCamera;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    public void OnPausedState(GameFlowService.PausedState state)
    {
        //throw new System.NotImplementedException();
    }

    public void OnReceiveInputState(GameFlowService.InputState state)
    {
        // throw new System.NotImplementedException();
    }

    public void OnWindowState(GameFlowService.WindowState state)
    {
        if (state == GameFlowService.WindowState.Main)
        {
            //Refresh();
        }
    }

    public void Refresh()
    {
        FadeAllOutline();
    }

    void FadeAllOutline()
    {
        //  foreach (var island in IslandBehaviour.islands)
        //  {
        //      island?.SetOutlineThin();
        //  }
    }

    private ClickTargetBehaviour GetPointerTarget(PointerEventData eventData)
    {
        RaycastHit raycastHit;
        Ray ray = MainCamera.ScreenPointToRay(eventData.position);  //Check for mouse click  touch?
                                                                    //Debug.Log("eventData.position! ");
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.transform != null)
            {
                //Debug.Log("transform! ");
                var go = raycastHit.transform.gameObject;
                var ct = go.GetComponent<ClickTargetBehaviour>();
                return ct;
            }
        }
        return null;
    }

    public void InputPanelClick(PointerEventData eventData)
    {
        var ct = GetPointerTarget(eventData);
        var dragging = InventoryBehaviour.instance.isDraging;
        if (ct != null)
        {
            ct.OnClicked();
            return;
        }

        if (dragging)
        {
            return;
        }
        ClickToMove.instance.CheckClick();
    }

    public void InputPanelRelease(PointerEventData eventData)
    {
        if (GameFlowService.instance.windowState == GameFlowService.WindowState.Main)
        {
            //FadeAllOutline();
        }
    }

    public void InputPanelDown(PointerEventData eventData)
    {
        if (GameFlowService.instance.windowState != GameFlowService.WindowState.Main)
        {
            return;
        }

        var ct = GetPointerTarget(eventData);
        if (ct != null)
        {
            //ct.SetOutlineThick();
            return;
        }
    }
}