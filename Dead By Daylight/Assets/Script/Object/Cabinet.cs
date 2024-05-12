using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    public bool isEmpty; // 생존자가 있는지 없는지 확인 하는 변수

    private SurvivorStat survivorStat;

    private void Start()
    {
        isEmpty = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        // 생존자의 경우
        if(collision.gameObject.tag== "Survivor")
        {
            // 캐비넷이 비어있다면
            if (isEmpty == true)
            {
                // space를 누를 경우 
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // 안에 느리게 들어 가는 애니메이션
                    // 안에 사람이 있기 때문에, false로 바꾸고 해당 생존자의 정보를 가져온다.
                    isEmpty= false;
                    survivorStat = collision.gameObject.GetComponent<SurvivorStat>();
                }
                 
                // shift와 space를 같이 누를 경우
                if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space) )
                {
                    // 빠르게 들어가는 애니메이션
                    isEmpty = false;
                    survivorStat = collision.gameObject.GetComponent<SurvivorStat>();
                }
            }
            // 캐비넷이 비어있지 않다면
            else
            {
                // 생존자 자기자신이 갇혀있는 경우
                if (survivorStat.gameObject == collision.gameObject)
                {
                    // space를 누를 경우
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        // 느리게 나오는 애니메이션
                        isEmpty = true;
                        survivorStat = null;
                    }

                    if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
                    {
                        // 빠르게 나오는 애니메이션
                        isEmpty = true;
                        survivorStat = null;
                    }
                }

                // 열었다가 닫는 애니메이션 추가
            }
            
        }


        // 살인마의 경우
        if (collision.gameObject.tag == "Killer")
        { 
            // 비어있는 곳에
            if(isEmpty == true)
            {
                // space를 누른 경우
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    // 열었다가 닫는 애니메이션
                }
                
            }
            // 생존자가 있다면
            else
            { 
               // space를 누를경우
               if(Input.GetKeyDown(KeyCode.Space))
               {
                    // 생존자를 업는 애니메이션
                   
                    KillerStat killerStat = collision.gameObject.GetComponent<KillerStat>();
                    // 생존자는 살인마에게 잡혔기 때문에
                    // 생존자의 건강상태는 0이된다.
                    survivorStat.health = 0;

                    // 생존자를 업고있는 상태로 바꾼다.
                    killerStat.isCarry = true;
               }
            }
        }

    }
}
