using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    // 판자를 연속으로 생존자가 넘을수 없게 하기 위한 변수
    public bool isUse;
    public float curUseCount;
    public float maxUseCount;


    private void Start()
    {
        isUse = false;
        curUseCount = 0;
        maxUseCount = 3;
    }

    private void Update()
    {
        // 누군가 이용했다면
        if (isUse == true)
        {
            curUseCount += Time.deltaTime;
            // maxCout이후에 이용가능하게 한다.
            if (curUseCount > maxUseCount)
            {
                isUse = false;
                curUseCount = 0;
            }
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        // 생존자의 경우
        if (collision.gameObject.tag == "Survivor")
        {
            // 누가 사용중이지 않은 경우
            if(isUse == false)
            {
                // space를 누르면
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // 느리게 넘어가는 애니메이션
                    isUse = true;
                }

                // shift + space를 누르면
                if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
                {
                    // 빠르게 넘어가는 애니메이션
                    isUse = true;
                }
            }
        }


        // 살인마의 경우
        if (collision.gameObject.tag == "Killer")
        {
            // space를 누르면
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // 넘어가는 애니메이션
                isUse = true;
            }
        }

    }
}
