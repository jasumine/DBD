using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AISWarriosStates
{
    TraceKiller,
    Attack,
    Die
}




public class AIWarriorsMode : MonoBehaviour
{
    public AISWarriosStates aiWarrios;
    SurvivorStat surv_Stat;

    public float findKillerRange;
    public float attackKillerRange;
    public bool isAttack;

    private bool isAttackCoroutine;
    private bool isDieCoroutine;

    public Collider surv_Weapon;
    public GameObject target;

    private void Start()
    {
        // 살인마를 찾아서 target에 넣어준다.
        FindKiller();
        isAttack = false;
        isAttackCoroutine = false;
        isDieCoroutine = false;
        surv_Stat = GetComponent<SurvivorStat>();
        surv_Stat.surRigid = GetComponent<Rigidbody>();
        surv_Stat.survAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(surv_Stat.isAI==true)
        {
            if (surv_Stat.health <= 0)
            {
                aiWarrios = AISWarriosStates.Die;
            }
            else
            {
                CheckAttackRange();
                if (isAttack == true)
                {
                    aiWarrios = AISWarriosStates.Attack;
                }
                else
                {
                    aiWarrios = AISWarriosStates.TraceKiller;
                }
            }
            AiWarriosState();
        }

    }


    private void AiWarriosState()
    {
        switch (aiWarrios)
        {
            case AISWarriosStates.TraceKiller:
                // 살인마를 향해 달려간다.
                TraceKiller();
                // isAttack이 false면 달리는 애니메이션
                surv_Stat.survAnimator.SetBool("BAttack", isAttack);
                break;

            case AISWarriosStates.Attack:
                // 살인마와 가깝다면 살인마를 타격한다.
                AttackKiller();
                break;

            case AISWarriosStates.Die:
                // hp가 없다면 죽는다.
                surv_Stat.survAnimator.SetBool("BAttack", false);
                Die();
                break;
        }
    }


    private void FindKiller()
    {
        // 범위내에 살인마가 있는지 찾는다.
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, findKillerRange);

        for(int i = 0;i<colliders.Length;i++)
        {
            // 범위안에 살인마가 있다면 target에 넣어준다
            if (colliders[i].tag=="Killer")
            {
                target = colliders[i].gameObject;
                aiWarrios = AISWarriosStates.TraceKiller;
                break;
            }
        }

    }


    // 살인마가 어택범위에 있는지 확인하는 함수
    private void CheckAttackRange()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, attackKillerRange);

        for(int i = 0;i<colliders.Length;i++)
        {
            if (colliders[i].tag == "Killer")
            {
                // 범위안에 있다면 true지만, 없다면 false
                isAttack = true;
                break;
            }
            else
            {
                isAttack = false;
            }
        }
    }



    private void TraceKiller()
    {
        Debug.Log("살인마를 향해 달린다. TraceKiller");
        // 살인마를 향해 달린다.
        surv_Stat.surPos = transform.position;
        surv_Stat.surPos = Vector3.MoveTowards(surv_Stat.surPos, target.transform.position, (surv_Stat.moveSpeed) * Time.deltaTime);
        transform.position = surv_Stat.surPos;
        transform.LookAt(target.transform.position);
    }

    private void AttackKiller()
    {
        // isAttack이 true면 때리는 애니메이션
        if(isAttackCoroutine == false)
        {
            StartCoroutine("CAttackKiller");
        }
    }

    IEnumerator CAttackKiller()
    {
        isAttackCoroutine = true;
        Debug.Log("살인마를 때린다. AttackKiller");

        transform.LookAt(target.transform.position);
        surv_Stat.survAnimator.SetBool("BAttack", isAttack);

        yield return new WaitForSeconds(0.45f);
        surv_Weapon.enabled = true;
        yield return new WaitForSeconds(0.15f);
        surv_Weapon.enabled = false;

        yield return new WaitForSeconds(0.05f);
        isAttackCoroutine = false;
    }


    // 생존자 죽음 처리 함수
    private void Die()
    {
        if(isDieCoroutine== false)
        {
            StopAllCoroutines();  
            StartCoroutine("CDie");
        }
    }

    IEnumerator CDie()
    {
        isDieCoroutine = true;

        Debug.Log("hp없음 사망");
        surv_Stat.survAnimator.SetTrigger("TDie");

        yield return new WaitForSeconds(1.5f);

        isDieCoroutine = false;
        this.gameObject.SetActive(false);
    }



    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(this.transform.position, 100f);

        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(this.transform.position, 1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, findKillerRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, attackKillerRange);
    }


}
