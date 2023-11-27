using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public float repairValue;
    public float damageValue;

    public bool isComplete = false;
    public bool isDamaged = false;

    private void Start()
    {
        repairValue = 0;
        damageValue = 0;
    }

    private void Update()
    {
        if(isDamaged)
        {
            ReduceRepairValue();
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (isComplete == false)
        {
            if (collision.gameObject.tag == "Survivor")
            {
                // 생존자인 경우 발전기 수리
                if (Input.GetMouseButton(0))
                {
                    isDamaged = false;
                    repairValue += 30f * Time.deltaTime;
                    if (repairValue >= 100)
                    {
                        isComplete = true;
                    }
                }
                
            }

            if (collision.gameObject.tag == "Killer")
            {
                // 살인마인 경우 발전기 손상
                if (Input.GetKey(KeyCode.Space) && !isDamaged)
                {
                    damageValue += 5f * Time.deltaTime;
                    if (damageValue >= 2)
                    {
                        isDamaged = true;
                    }
                    else
                    {
                        damageValue = 0;
                    }
                }
            }
        }
    }

    private void ReduceRepairValue()
    {
        repairValue -= 0.1f * Time.deltaTime;
    }
}
