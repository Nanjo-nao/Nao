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
        if (id == "ezio")
        {
            JumpingRaceSystem.instance.OnBetEzio();
            return;
        }

        if (id == "geralt")
        {
            JumpingRaceSystem.instance.OnBetGeraltWin();
            return;
        }

        if (id == "plant" && _plant == null && (data != null && (data.id == "psht" || data.id == "fsht")))
        {
            PlantAPlant(data.id);
            return;
        }

        if (id == "kabi" && data != null && data.id == "flute")
        {
            KabiSystem.instance.RemoveKabi();
            StopDraging(data.id);
            return;
        }
    }

    void StopDraging(string id)
    {
        InventoryService.instance.RemoveItem(id, 1);
        InventoryBehaviour.instance.SyncItems();
        InventoryService.instance.StopItemDraging();
    }

    void PlantAPlant(string id)
    {
        Debug.Log("PlantAPlant " + id);
        _plant = PvzService.instance.Plant(this.transform, id);
    }
}
