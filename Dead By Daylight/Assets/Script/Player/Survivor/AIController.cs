using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStates
{
    FindGenerator,
    RepairGenerator,

    FindSurvivor,
    SaveSurvivor,
    CareSurvivor,

    RunToTarget,
    WalkToTarget,
    Sit,

    Trace
}

public class AIController : MonoBehaviour
{
    public AIStates aiState;
    SurvivorStat surv_Stat;

    public int killerRangeIn = 24;
    public int killerRangeOut = 16;
    public int killerRangeStep;

    public GameObject target;

    private SurvivorStat friendStat;

    Vector3 aiPos;

    private void Start()
    {
        surv_Stat = GetComponent<SurvivorStat>();
        surv_Stat.surRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(surv_Stat.isAI==true)
        {
            AiState();
        }
    }

    private void FixedUpdate()
    {
        if (surv_Stat.isAI == true)
        {
            CheckAiObject();
            CheckAIHealthState();
            CheckKillerPos();
        }
    }

    private void AiState()
    {
        switch (aiState)
        {
            case AIStates.FindGenerator:
                // 범위안에 가장 가까운 generator를 찾아간다.
                AIFindGenerator();
                break;
            case AIStates.RepairGenerator:
                // overlap으로 repair실행
                break;

            case AIStates.FindSurvivor:
                AIFindFriend();
                break;

            case AIStates.CareSurvivor:
                AICareSurvivor();
                break;

            case AIStates.RunToTarget:
                AIGoToTarget(1f);
                break;
            case AIStates.WalkToTarget:
                AIGoToTarget(0.5f);
                break;
            case AIStates.Sit:
                AISit();
                break;

            case AIStates.Trace:
                break;
        }
    }


    private void CheckAIHealthState()
    {
        // 주위에 살인마가 없다면 행동하러 다닌다.
        if(killerRangeStep==0||killerRangeStep ==1)
        {
            switch (surv_Stat.health)
            {
                case 2:
                    aiState = AIStates.FindGenerator;
                    break;
                case 1:
                    if (target == null || target.tag != "Survivor")
                        aiState = AIStates.FindSurvivor;
                    else
                        aiState = AIStates.RunToTarget;
                    break;
                case 0:
                    // hp가 0이면, 

                    break;
            }
        }
    }

    private void CheckKillerPos()
    {
        // 주위에 살인마가 있다면 step으로 관리하기
        // ~8/ 8~16 / 16~24/ 24~ -> 3/2/1/0 단계
        switch(killerRangeStep)
        {
            // 살인마가 24m밖에있다. 안전하다.
            case 0:
                KillerRange24();
                break;
            // 살인마가 16m 밖 ~ 24m 안에 있다. 걸어간다.
            case 1:
                aiState = AIStates.WalkToTarget;
                KillerRange16();
                break;
            // 살인마가 8m 밖 ~ 16m 안에 있다. 숨는다
            case 2:
                aiState = AIStates.Sit;
                KillerRange8();
                break;
            // 살인마가 8m 안에 있다. 들켰으니까 달린다.
            case 3:
                aiState = AIStates.Trace;
                KillerRange8();
                break;

        }

    }


    private void KillerRange24()
    {
        killerRangeIn = 24;
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, killerRangeIn);

        // 24, 16, 8

        for(int i = 0; i<colliders.Length; i++)
        {
            // 범위안에 killer가 있다면,
            if (colliders[i].tag =="Killer")
            {
                // 16~24이므로, step1
                //Debug.Log("killer pos is 16~24");
                killerRangeStep = 1;
                return;
            }
        }
        killerRangeStep = 0;
    }

    private void KillerRange16()
    {
        killerRangeIn = 24;
        killerRangeOut = 16;
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, killerRangeIn);
        Collider[] colliderss = Physics.OverlapSphere(this.transform.position, killerRangeOut);

        
        // 16안에 들어오면, 8~16을 검사해야해서 step 2로 넘어간다.
        for(int i=0;i<colliderss.Length;i++)
        {
            if (colliderss[i].tag == "Killer")
            {
                //Debug.Log("killer pos is ~16");
                killerRangeStep = 2;
                return;
            }
        }

        // 16안에 들어오지 않았다면 다음 for문에 오고, 16~24사이에 살인마가 있다면 step은 여전히 1이다.
        for (int i = 0;i<colliders.Length; i++)
        {
            // 범위안에 killer가 있다면,
            if (colliders[i].tag == "Killer")
            {
                //Debug.Log("killer pos is 16~24");
                killerRangeStep = 1;
                return;
            }
        }

        // 24안에 없다면 함수의 끝까지 오게되어서, step0이된다.
        //Debug.Log("killer pos is 24~");
        killerRangeStep = 0;
    }

    private void KillerRange8()
    {
        killerRangeIn = 16;
        killerRangeOut = 8;
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, killerRangeIn);
        Collider[] colliderss = Physics.OverlapSphere(this.transform.position, killerRangeOut);


        // 8안에 들어오면, ~8을 검사해야해서 step 3로 넘어간다.
        for (int i = 0; i < colliderss.Length; i++)
        {
            if (colliderss[i].tag == "Killer")
            {
                //Debug.Log("killer pos is ~8");
                killerRangeStep = 3;
                return;
            }
        }

        // 8안에 들어오지 않았다면 다음 for문에 오고, 8~16사이에 살인마가 있다면 step은 여전히 2이다.
        for (int i = 0; i < colliders.Length; i++)
        {
            // 범위안에 killer가 있다면,
            if (colliders[i].tag == "Killer")
            {
                //Debug.Log("killer pos is 8~16");
                killerRangeStep = 2;
                return;
            }
        }

        // 16안에 없다면 함수의 끝까지 오게되어서, step1이된다.
        //Debug.Log("killer pos is 16~24");
        killerRangeStep = 1;
    }



    private void AIFindGenerator()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 40f);
        for(int i = 0; i<colliders.Length; i++)
        {
            if (colliders[i].tag =="Generator")
            {
                Generator generator = colliders[i].GetComponent<Generator>();
                if(generator.isComplete == false)
                {
                    target = colliders[i].gameObject;
                    aiState = AIStates.RunToTarget;
                    return;
                }
            }
        }

        // 주위에 발전기가 없으면 return되지 않고 for문이 끝나게 된다.
        // 다시 발전기를 찾으러 간다.
        // aiState = AIStates.FindGenerator;

    }

   
    private void AIFindFriend()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 100f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Survivor")
            {
                target = colliders[i].gameObject;
                aiState = AIStates.RunToTarget;
                return;
            }
        }
    }


    private void AIGoToTarget(float _speed)
    {
        // 목적지까지 달려간다.
        surv_Stat.surPos = transform.position;
        surv_Stat.surPos = Vector3.MoveTowards(surv_Stat.surPos, target.transform.position, (surv_Stat.moveSpeed * _speed) * Time.deltaTime);
        transform.position = surv_Stat.surPos;

        CheckAiObject();
        // 네비게이션 -> 최소범위 넣어주기
        // 최소범위를 정해서 근처에 있으면 실행되도록하기.

    }



    private void AISit()
    {
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.yellow;
    }


    private void CheckAiObject()
    {
        // 오버랩으로, 내 앞에 있는 obejct에 따라 행동을 바꾼다.
        //Collider[] colliders = Physics.OverlapBox(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.5f), this.transform.localScale, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 3f);

        for (int i = 0; i < colliders.Length; i++)
        {
            // 앞에 발전기가 있다면 수리를 한다.
            if (colliders[i].tag == "Generator")
            {
                aiState = AIStates.RepairGenerator;
                AIRepairGenerator(colliders[i].gameObject);
                break;
            }
            // 앞에 다친 생존자가 있다면 치료를 해준다.
            else if (colliders[i].tag == "Survivor")
            {
                if (colliders[i].gameObject == this.gameObject)
                    continue;

                friendStat = colliders[i].GetComponent<SurvivorStat>();
                if (friendStat.health <= 1)
                {
                    aiState = AIStates.CareSurvivor;
                }
                else
                    continue;
            }
        }
    }

    private void AIRepairGenerator(GameObject _object)
    {
        if(aiState== AIStates.RepairGenerator)
        {
            Renderer render = GetComponent<Renderer>();
            render.material.color = Color.gray;
            Generator generator = _object.GetComponent<Generator>();

            generator.repairValue += 0.5f * Time.deltaTime;
            if (generator.repairValue >= 100)
            {
                generator.CompleteGenerator();
                aiState = AIStates.FindGenerator;
            }
        }
    }

    private void AICareSurvivor()
    {
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.gray;
        // friendStat에 접근해서 치료하기.
        friendStat.currentHealth += surv_Stat.speedFriendCare * Time.deltaTime;
        if (friendStat.currentHealth >= friendStat.maxHealth)
        {
            friendStat.currentHealth = 0;
            friendStat.health++;
            friendStat = null;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 100f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, 1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, killerRangeIn);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, killerRangeOut);
    }

}
