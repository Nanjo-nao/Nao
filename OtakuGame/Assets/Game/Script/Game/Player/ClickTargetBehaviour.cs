using UnityEngine;
using System.Collections;

public class ClickTargetBehaviour : MonoBehaviour
{
    public string id;
    public bool stillNavHere;

    public void OnClicked()
    {
        Debug.Log("ClickTarget " + gameObject + " " + id + " isDraging " + InventoryBehaviour.instance.isDraging);
    }
}
