using UnityEngine;

public class ItemCollectBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            InventoryService.instance.AddItem("flute", 1);
            InventoryBehaviour.instance.Show();
            Destroy(gameObject);
        }
    }
}
