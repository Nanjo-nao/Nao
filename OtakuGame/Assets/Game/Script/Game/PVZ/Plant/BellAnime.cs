using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellAnime : MonoBehaviour
{
    public BellShooter bellShooter;
    
    public void Fire()
    {
        //Debug.Log("Fire!!!!");
        bellShooter.Fire();
    }
}
