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
        Debug.Log("ClickTarget " + gameObject + " " + id);

        if (id == "plant" && _plant == null && (data != null && (data.id == "psht" || data.id == "fsht")))
        {
            PlantAPlant(data.id);
        }
    }

    void PlantAPlant(string id)
    {
        Debug.Log("PlantAPlant " + id);
        _plant = PvzService.instance.Plant(this.transform, id);
    }
}
