using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // ���� collider �� ������ �����ڶ��, �������� setHealth�Լ��� �����Ѵ�.
    // survivor.Sethealth() �Լ��� �������� hp�� -- �ϴ� �Լ�.

    public Animator killerAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Survivor")
        {
            SurvivorController survivor = other.GetComponent<SurvivorController>();

            // �������� hp�� �𿴴��� Ȯ���ؼ� ���� �������θ� �Ǵ�
            if(survivor.Sethealth() == true)
            {
                killerAnimator.SetBool("BAttackSuccess", true);
                Invoke("DelayBool", 3f);
            }
        }
    }

    private void DelayBool()
    {
        killerAnimator.SetBool("BAttackSuccess", false);
    }
}
