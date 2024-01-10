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
                // �����ȿ� ���� ����� generator�� ã�ư���.
                AIFindGenerator();
                break;
            case AIStates.RepairGenerator:
                // overlap���� repair����
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
        // ������ ���θ��� ���ٸ� �ൿ�Ϸ� �ٴѴ�.
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
                    // hp�� 0�̸�, 

                    break;
            }
        }
    }

    private void CheckKillerPos()
    {
        // ������ ���θ��� �ִٸ� step���� �����ϱ�
        // ~8/ 8~16 / 16~24/ 24~ -> 3/2/1/0 �ܰ�
        switch(killerRangeStep)
        {
            // ���θ��� 24m�ۿ��ִ�. �����ϴ�.
            case 0:
                KillerRange24();
                break;
            // ���θ��� 16m �� ~ 24m �ȿ� �ִ�. �ɾ��.
            case 1:
                aiState = AIStates.WalkToTarget;
                KillerRange16();
                break;
            // ���θ��� 8m �� ~ 16m �ȿ� �ִ�. ���´�
            case 2:
                aiState = AIStates.Sit;
                KillerRange8();
                break;
            // ���θ��� 8m �ȿ� �ִ�. �������ϱ� �޸���.
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
            // �����ȿ� killer�� �ִٸ�,
            if (colliders[i].tag =="Killer")
            {
                // 16~24�̹Ƿ�, step1
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

        
        // 16�ȿ� ������, 8~16�� �˻��ؾ��ؼ� step 2�� �Ѿ��.
        for(int i=0;i<colliderss.Length;i++)
        {
            if (colliderss[i].tag == "Killer")
            {
                //Debug.Log("killer pos is ~16");
                killerRangeStep = 2;
                return;
            }
        }

        // 16�ȿ� ������ �ʾҴٸ� ���� for���� ����, 16~24���̿� ���θ��� �ִٸ� step�� ������ 1�̴�.
        for (int i = 0;i<colliders.Length; i++)
        {
            // �����ȿ� killer�� �ִٸ�,
            if (colliders[i].tag == "Killer")
            {
                //Debug.Log("killer pos is 16~24");
                killerRangeStep = 1;
                return;
            }
        }

        // 24�ȿ� ���ٸ� �Լ��� ������ ���ԵǾ, step0�̵ȴ�.
        //Debug.Log("killer pos is 24~");
        killerRangeStep = 0;
    }

    private void KillerRange8()
    {
        killerRangeIn = 16;
        killerRangeOut = 8;
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, killerRangeIn);
        Collider[] colliderss = Physics.OverlapSphere(this.transform.position, killerRangeOut);


        // 8�ȿ� ������, ~8�� �˻��ؾ��ؼ� step 3�� �Ѿ��.
        for (int i = 0; i < colliderss.Length; i++)
        {
            if (colliderss[i].tag == "Killer")
            {
                //Debug.Log("killer pos is ~8");
                killerRangeStep = 3;
                return;
            }
        }

        // 8�ȿ� ������ �ʾҴٸ� ���� for���� ����, 8~16���̿� ���θ��� �ִٸ� step�� ������ 2�̴�.
        for (int i = 0; i < colliders.Length; i++)
        {
            // �����ȿ� killer�� �ִٸ�,
            if (colliders[i].tag == "Killer")
            {
                //Debug.Log("killer pos is 8~16");
                killerRangeStep = 2;
                return;
            }
        }

        // 16�ȿ� ���ٸ� �Լ��� ������ ���ԵǾ, step1�̵ȴ�.
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

        // ������ �����Ⱑ ������ return���� �ʰ� for���� ������ �ȴ�.
        // �ٽ� �����⸦ ã���� ����.
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
        // ���������� �޷�����.
        surv_Stat.surPos = transform.position;
        surv_Stat.surPos = Vector3.MoveTowards(surv_Stat.surPos, target.transform.position, (surv_Stat.moveSpeed * _speed) * Time.deltaTime);
        transform.position = surv_Stat.surPos;

        CheckAiObject();
        // �׺���̼� -> �ּҹ��� �־��ֱ�
        // �ּҹ����� ���ؼ� ��ó�� ������ ����ǵ����ϱ�.

    }



    private void AISit()
    {
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.yellow;
    }


    private void CheckAiObject()
    {
        // ����������, �� �տ� �ִ� obejct�� ���� �ൿ�� �ٲ۴�.
        //Collider[] colliders = Physics.OverlapBox(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.5f), this.transform.localScale, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 3f);

        for (int i = 0; i < colliders.Length; i++)
        {
            // �տ� �����Ⱑ �ִٸ� ������ �Ѵ�.
            if (colliders[i].tag == "Generator")
            {
                aiState = AIStates.RepairGenerator;
                AIRepairGenerator(colliders[i].gameObject);
                break;
            }
            // �տ� ��ģ �����ڰ� �ִٸ� ġ�Ḧ ���ش�.
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
        // friendStat�� �����ؼ� ġ���ϱ�.
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
