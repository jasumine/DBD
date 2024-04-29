using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryBack : MonoBehaviour
{
    KillerStat killer_Stat;

    private SurvivorController survivorController;
    private GameObject survivor;
    private Hook hook;

    Collider checkBox;

    void Start()
    {
        checkBox = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        // 생존자를 업을수 있는 상태일때, space를 누르면 생존자를 들 수 있다.
        if (killer_Stat.isCarryCan == true && Input.GetKeyDown(KeyCode.Space))
        {
            // 생존자가 움직일 수 없도록 한다.
            survivorController.isSurvMove = false;
            survivorController.isCarried = true;

            // 생존자를 업고 같이 이동해야함.
            killer_Stat.isCarry = true;
        }

        // 생존자가 업혀있는 상태라면
        if(killer_Stat.isCarry ==true)
        {
            // 두명의 생존자를 들 수 없도록 fal해주고
            // 생존자의 위치가 킬러의 위치랑 같이 이독되도록 해준다.
            killer_Stat.isCarryCan = false;
            survivor.transform.position = transform.position + killer_Stat.offset;

            // 움직이는 동안 충돌이 일어나지 않고, 중력의 힘을 받아 아래로 내려가지 않게 해준다.
            // 계속 충돌되지 않게 하기 위해 collider를 꺼준다.
            Collider collider = survivor.gameObject.GetComponent<Collider>();
            Rigidbody rigid = survivor.gameObject.GetComponent<Rigidbody>();
            collider.enabled = false;   
            rigid.useGravity = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // 생존자와 부딪혀 있는 동안
        if (collision.gameObject.tag == "Survivor")
        {
            // 해당 생존자의 정보를 가져온다.
            survivorController = collision.gameObject.GetComponent<SurvivorController>();

            SurvivorStat survStat = survivorController.gameObject.GetComponent<SurvivorStat>();

            survivor = survivorController.gameObject;

            // 해당 생존자의 건강상태가 0이고, 살인마가 누군가를 들고 있지 않을때
            // 살인마는 해당 생존자를 업을 수 있도록 활성화 된다.
            if (survStat.health == 0 && killer_Stat.isCarry == false)
            {
                killer_Stat.isCarryCan = true;
            }
            else
            {
                killer_Stat.isCarryCan = false;
            }
        }

        // 갈고리와 부딪힌다면
        if(collision.gameObject.tag == "Hook")
        {
            // 해당 갈고리의 정보를 받아온다.
            hook = collision.gameObject.GetComponent<Hook>();  

            // 갈고리에 누군가 걸려있지 않다면(비어있다)
            // space를 누르면 갈고리에 생존자를 거는 함수를 실행한다.
            if(hook.GetIsHanging() == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    HangOnHook();
                }
            }
        }
    }


    // 갈고리와 상호작용 하는 함수
    private void HangOnHook()
    {
        Debug.Log("hangOnHook");
        // 살인마는 생존자를 들고 있지 않은 상태가 되고,
        // 살인마의 위치는 갈고리위치를 기준으로 걸린상태가 된다.
        killer_Stat.isCarry = false;
        survivor.transform.position = hook.transform.position + new Vector3(0, 0.5f, -0.5f);

        // 후크에 생존자의 정보를 넣어준다.
        hook.SetIsHanging(survivorController);

        // 생존자의 collider를 활성화 시키고 rigidbody를 비활성화 시킨다(중력작용X) 
        Collider collider = survivor.gameObject.GetComponent<Collider>();
        Rigidbody rigid = survivor.gameObject.GetComponent<Rigidbody>();
        collider.enabled = true;
        rigid.useGravity = false;

        // 생존자를 걸었기 때문에, 살인마는 더이상 생존자의 정보를 가지지 않도록 비워주고
        // 살인마는 생존자를 들 수 없도록 바꾸어준다.
        survivorController = null;
        survivor = null;
        killer_Stat.isCarryCan = false;
    }



    private void OnCollisionExit(Collision collision)
    {
        // 생존자와 충돌했다가 벗어날 경우 
        // 해당 생존자의 정보를 계속 가지고 있으면 안되기 때문에 null을 해준다.
        if (collision.gameObject.tag == "Survivor")
        {
            survivorController = null;
            survivor = null;
            killer_Stat.isCarryCan = false;
        }
    }
}
