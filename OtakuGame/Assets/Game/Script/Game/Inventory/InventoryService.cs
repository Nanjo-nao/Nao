using UnityEngine;
using System.Collections.Generic;

public class InventoryService : MonoBehaviour
{
    public static InventoryService instance { get; private set; }

    public List<ItemData> items;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ClearItems();

        Test();
    }

    void Test()
    {
        AddItem("coin", 2);
        AddItem("fsht", 1);
        AddItem("psht", 3);
        AddItem("flute", 2);
        RemoveItem("flute", 1);

        Debug.Log(items);
        InventoryBehaviour.instance.Show();
    }

    public ItemPrototype GetPrototype(string id)
    {
        foreach (var i in ConfigService.instance.itemsConfig.list)
        {
            if (i != null && i.id == id)
            {
                return i;
            }
        }
        return null;
    }

    public void AddItem(ItemData item)
    {
        AddItem(item.id, item.n);
    }

    public void AddItem(string id, int n)
    {
        foreach (var i in items)
        {
            if (i.id == id)
            {
                i.n += n;
                return;
            }
        }

        items.Add(new ItemData(n, id));
    }

    public void ClearItems()
    {
        items = new List<ItemData>();
    }

    public bool RemoveItem(ItemData item)
    {
        return RemoveItem(item.id, item.n);
    }

    public bool RemoveItem(string id, int n)
    {
        foreach (var i in items)
        {
            if (i.id == id)
            {
                i.n -= n;
                if (i.n == 0)
                {
                    items.Remove(i);
                }
                return true;
            }
        }

        return false;
    }

    private SlotBehaviour _dragingSlot;

    public void StartItemDraging(SlotBehaviour slot)
    {
        _dragingSlot = slot;

        var proto = GetPrototype(slot.data.id);
        InventoryBehaviour.instance.SetSp(proto.sp);
        InventoryBehaviour.instance.StartDrag();
        slot.ShowRays();
    }

    public void ValidateItemDraging()
    {
        var target = GetItemDragTarget();
        if (target != null)
        {
            var targetCanReact = false;


            if (targetCanReact)
            {
                StopItemDraging();
                return;
            }
        }

        CancelDraging();
    }

    void CancelDraging()
    {
        StopItemDraging();//TODO smoothly return the item instead of cut
    }

    public void TryStopDraging()
    {
        if (!InventoryBehaviour.instance.isDraging)
        {
            return;
        }

        StopItemDraging();
    }
    public void StopItemDraging()
    {
        InventoryBehaviour.instance.StopDrag();
        _dragingSlot.HideRays();
        _dragingSlot = null;
    }

    public ClickTargetBehaviour GetItemDragTarget()
    {
        return null;
    }
}
