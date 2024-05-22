using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorWarriorsWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killer")
        {
            KillerController killer = other.GetComponent<KillerController>();

            // 살인마의 hp감소
            killer.SetKillerHealth();
        }
    }
}
