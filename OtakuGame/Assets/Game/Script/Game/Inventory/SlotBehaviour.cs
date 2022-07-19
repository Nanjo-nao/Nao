using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotBehaviour : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI titleTxt;
    public TextMeshProUGUI numTxt;

    public GameObject rays;

    public ItemData data { get; private set; }

    public void Start()
    {
        Hide();
    }

    public void Hide()
    {
        //Debug.Log("Hide");
        data = null;
        gameObject.SetActive(false);
        HideRays();
    }

    public void Show(ItemData item)
    {
        data = item;
        if (item == null)
        {
            Hide();
            return;
        }

        var proto = InventoryService.instance.GetPrototype(item.id);
        Debug.Log("slot " + item.id);
        img.sprite = proto.sp;
        titleTxt.SetText(proto.title);
        numTxt.SetText(item.n + "");
        gameObject.SetActive(true);
    }

    public void OnClick()
    {
        if (data == null)
        {
            return;
        }
        var proto = InventoryService.instance.GetPrototype(data.id);
        switch (proto.usage)
        {
            case ItemPrototype.Usage.None:
                break;

            case ItemPrototype.Usage.Plant:
                InventoryService.instance.StartItemDraging(this);
                break;

            case ItemPrototype.Usage.Flute:
                InventoryService.instance.StartItemDraging(this);
                break;

            case ItemPrototype.Usage.Token:
                InventoryService.instance.StartItemDraging(this);
                break;
        }
    }

    public void ShowRays()
    {
        rays.SetActive(true);
    }

    public void HideRays()
    {
        rays.SetActive(false);
    }
}
