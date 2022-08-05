using UnityEngine;

public class ErdTreeTriggerEnterBehaviour : MonoBehaviour
{
    public string eventName;
    public bool hideAfterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<ClickToMove>();
        if (player != null)
        {
            switch (eventName)
            {
                case "Knee":
                    EldTreeSystem.instance.Knee();
                    if (hideAfterTrigger)
                        gameObject.SetActive(false);
                    break;

                case "SeeTreeFinger":
                    EldTreeSystem.instance.SeeTreeFinger();
                    break;

                case "MeetAgain":
                    EldTreeSystem.instance.MeetAgain();
                    break;
            }
        }
    }
}
