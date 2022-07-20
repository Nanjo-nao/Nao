using UnityEngine;
using System.Collections;

public class ClickTargetBehaviour : MonoBehaviour
{
    public string id;
    public bool stillNavHere;

    private PlantBehaviour _plant;

    public void OnClicked(ItemData data)
    {
        //Debug.Log("ClickTarget " + gameObject + " " + id + " isDraging " + InventoryBehaviour.instance.isDraging);
        //Debug.Log("ClickTarget " + gameObject + " " + id + " " + data.id);
        //Debug.Log(id);
        //Debug.Log(data.id);
        //Debug.Log(id == "kabi");
        //Debug.Log(data.id == "flute");
        if (id == "plant" && _plant == null && (data != null && (data.id == "psht" || data.id == "fsht")))
        {
            PlantAPlant(data.id);
        }
        else if (id == "kabi" && data != null && data.id == "flute")
        {
            //Debug.Log("RemoveKabi ");
            GameSystem.instance.RemoveKabi();
            StopDraging(data.id);
        }
    }

    void StopDraging(string id)
    {
        InventoryService.instance.RemoveItem(id, 1);
        InventoryBehaviour.instance.SyncItems(InventoryService.instance.items);
        InventoryService.instance.StopItemDraging();
    }

    void PlantAPlant(string id)
    {
        Debug.Log("PlantAPlant " + id);
        _plant = PvzService.instance.Plant(this.transform, id);
    }
}
