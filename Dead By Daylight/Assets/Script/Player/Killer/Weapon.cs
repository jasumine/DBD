using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Survivor")
        {
            SurvivorController survivor = other.GetComponent<SurvivorController>();

            survivor.Sethealth();
        }
    }
}
