using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public float openValue;

    public bool isComplete = false;

    void Start()
    {
        openValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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

                    openValue += 30f * Time.deltaTime;
                    if (openValue >= 100)
                    {
                        isComplete = true;
                        GetItem();
                    }
                }
                else
                {
                    openValue = 0;
                }

            }
        }
    }

    private void GetItem()
    {
        // �������� �������� �����Ǿ����.
        // ������ �����ͺ��̽��� ���� �ϱ�
        Debug.Log("�������� ȹ���߽��ϴ�!");
    }
}
