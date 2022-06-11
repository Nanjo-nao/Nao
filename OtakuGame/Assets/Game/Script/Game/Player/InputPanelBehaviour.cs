using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using com;


public class InputPanelBehaviour : UIBehaviour, IEventSystemHandler, IPointerClickHandler
{
    public static InputPanelBehaviour instance { get; private set; }

    public RectTransform canvasTrans;

    private float _timestampTap;
    public float canvasScale { get; private set; }

    private Vector2 _delta;
    private Vector2 _dragStartPos;
    public bool IsDraging { get; private set; }
    private Vector2 startPos;

    protected InputPanelBehaviour()
    {
        //Debug.Log("InputPanelBehaviour");
    }

    protected override void Awake()
    {
        base.Awake();
        instance = this;
        canvasScale = canvasTrans.localScale.x;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            return;
        }
        InputPanelService.instance.InputPanelClick(eventData);
    }
}