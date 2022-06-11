using UnityEngine;
using System.Collections;

public class ClickTargetBehaviour : MonoBehaviour
{
    public string myName;

    void Update()
    {

    }

    public void OnClicked()
    {
        Debug.Log("OnClicked PointerTarget " + gameObject + " " + myName + " isDraging " + InventoryBehaviour.instance.isDraging);
    }
}
