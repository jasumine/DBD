using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Board : MonoBehaviour
{
    public bool isDown; // 판자가 내려갔는지 확인을 위한 변수

    // 판자를 연속으로 생존자가 넘을수 없게 하기 위한 변수
    public bool isUse;
    public float curUseCount;
    public float maxUseCount;

    private void Start()
    {
        isDown = false;
        isUse = false;
        curUseCount = 0;
        maxUseCount = 1;
    }

    private void Update()
    {
        // 누군가 이용했다면
        if(isUse == true)
        {
            curUseCount += Time.deltaTime;
            // maxCout이후에 이용가능하게 한다.
            if(curUseCount > maxUseCount)
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
            // 판자가 내려가 있지 않은 경우
            if(isDown == false)
            {
                // space를 누르면 판자를 내린다.
                if(Input.GetKeyDown(KeyCode.Space)) 
                {
                    BoardDown();
                }
            }

            // 판자가 내려간 경우
            else
            {
                // space만 누른 경우
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    // 판자를 천천히 이동하는 애니메이션

                    isUse = true;
                }

                // space와 shift를 함께 누른 경우
                if(Input.GetKeyDown (KeyCode.Space) && Input.GetKeyDown(KeyCode.LeftShift))
                {
                    // 판자를 빠르게 이동하는 애니메이션

                    isUse = true;
                }
            }
        }


        // 살인마의 경우
        if (collision.gameObject.tag == "Killer")
        {
            // 판자가 내려가있고, space를 누르면
            if(isDown==true  && Input.GetKeyDown(KeyCode.Space))
            {
                // 판자를 부순다.
                BoardBreak();
            }

        }

    }

    private void BoardDown()
    {
        isDown = true;
        // 판자가 넘어지는 애니메이션 실행

    }

    private void BoardBreak()
    {
        // 판자 부서지는 애니메이션 및 판자 삭제하기

        gameObject.SetActive(false);
    }

}
