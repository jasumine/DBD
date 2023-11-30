using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totem : MonoBehaviour
{
    public float totemValue;

    public bool isComplete;

    void Start()
    {
        totemValue = 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isComplete == false)
        {
            if (collision.gameObject.tag == "Survivor")
            {
                // �������� ��� ���� ����
                if (Input.GetMouseButton(0))
                {

                    totemValue += 30f * Time.deltaTime;
                    if (totemValue >= 100)
                    {
                        isComplete = true;
                        TotemAbility();
                    }
                }
                else
                {
                    totemValue = 0;
                }

            }
        }
    }

    private void TotemAbility()
    {
        Debug.Log("������ Ȱ��ȭ �Ǿ����ϴ�.");
    }

}
