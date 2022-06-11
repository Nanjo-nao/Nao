using UnityEngine;
using System.Collections;

public class ClickTargetBehaviour : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClicked()
    {
        Debug.Log("OnClicked PointerTarget " + gameObject);
    }
}
