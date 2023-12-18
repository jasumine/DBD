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
                // 생존자인 경우 상자 오픈
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
        // 아이템이 랜덤으로 생성되어야함.
        // 아이템 데이터베이스를 만들어서 하기
        Debug.Log("아이템을 획득했습니다!");
    }
}
