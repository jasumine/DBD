using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 무기 collider 에 닿은게 생존자라면, 생존자의 setHealth함수를 실행한다.
    // survivor.Sethealth() 함수는 생존자의 hp를 -- 하는 함수.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Survivor")
        {
            SurvivorController survivor = other.GetComponent<SurvivorController>();

            survivor.Sethealth();
        }
    }
}
