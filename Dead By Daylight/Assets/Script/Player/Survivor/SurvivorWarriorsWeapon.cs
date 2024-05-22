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

            // ���θ��� hp����
            killer.SetKillerHealth();
        }
    }
}
