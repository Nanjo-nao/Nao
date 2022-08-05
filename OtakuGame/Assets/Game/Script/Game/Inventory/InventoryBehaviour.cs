using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class InventoryBehaviour : MonoBehaviour
{
    public static InventoryBehaviour instance { get; private set; }

    public Image imgDrag;

    public bool isDraging { get; private set; }

    public CanvasGroup cg;

    public List<SlotBehaviour> slots;

    private void Awake()
    {
        instance = this;
        Hide();
    }

    void Start()
    {
        StopDrag();
    }

    void Update()
    {
        if (isDraging)
        {
            imgDrag.rectTransform.anchoredPosition = Input.mousePosition;
            if (Input.GetMouseButtonDown(1))
            {
                InventoryService.instance.StopItemDraging();
            }
        }
    }

    public void SetSp(Sprite sp)
    {
        imgDrag.sprite = sp;
    }

    public void StartDrag()
    {
        com.SoundService.instance.Play("drag");
        Debug.Log("StartDrag");
        isDraging = true;
        imgDrag.gameObject.SetActive(true);
        foreach (var slot in slots)
        {
            slot.HideRays();
        }
    }

    public void StopDrag()
    {
        com.SoundService.instance.Play("drop");
        Debug.Log("StopDrag");
        isDraging = false;
        imgDrag.gameObject.SetActive(false);
        foreach (var slot in slots)
        {
            slot.HideRays();
        }
    }

    public void Hide()
    {
        Debug.Log("--hide");
        cg.DOKill();
        //cg.alpha = 0;
        cg.DOFade(0, 0.6f);
        cg.blocksRaycasts = false;
        cg.interactable = false;
    }

    public void Show(bool instant = false)
    {
        Debug.Log("--Show");
        SyncItems();

        cg.DOKill();
        if (instant)
        {
            cg.alpha = 1;
        }
        else
        {
            cg.DOFade(1, 0.6f);
        }

        cg.blocksRaycasts = true;
        cg.interactable = true;
    }

    public void SyncItems()
    {
        SyncItems(InventoryService.instance.items);
    }

    public void SyncItems(List<ItemData> items)
    {
        int i = 0;
        foreach (var slot in slots)
        {
            ItemData data = null;
            if (i < items.Count)
            {
                data = items[i];
            }
            slot.Show(data);

            i++;
        }
    }
}
