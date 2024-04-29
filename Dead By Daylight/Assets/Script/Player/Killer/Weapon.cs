using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // ���� collider �� ������ �����ڶ��, �������� setHealth�Լ��� �����Ѵ�.
    // survivor.Sethealth() �Լ��� �������� hp�� -- �ϴ� �Լ�.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Survivor")
        {
            SurvivorController survivor = other.GetComponent<SurvivorController>();

            survivor.Sethealth();
        }
    }
}
