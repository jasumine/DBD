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
        // ���θ��� ã�Ƽ� target�� �־��ش�.
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
                // ���θ��� ���� �޷�����.
                TraceKiller();
                // isAttack�� false�� �޸��� �ִϸ��̼�
                surv_Stat.survAnimator.SetBool("BAttack", isAttack);
                break;

            case AISWarriosStates.Attack:
                // ���θ��� �����ٸ� ���θ��� Ÿ���Ѵ�.
                AttackKiller();
                break;

            case AISWarriosStates.Die:
                // hp�� ���ٸ� �״´�.
                surv_Stat.survAnimator.SetBool("BAttack", false);
                Die();
                break;
        }
    }


    private void FindKiller()
    {
        // �������� ���θ��� �ִ��� ã�´�.
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, findKillerRange);

        for(int i = 0;i<colliders.Length;i++)
        {
            // �����ȿ� ���θ��� �ִٸ� target�� �־��ش�
            if (colliders[i].tag=="Killer")
            {
                target = colliders[i].gameObject;
                aiWarrios = AISWarriosStates.TraceKiller;
                break;
            }
        }

    }


    // ���θ��� ���ù����� �ִ��� Ȯ���ϴ� �Լ�
    private void CheckAttackRange()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, attackKillerRange);

        for(int i = 0;i<colliders.Length;i++)
        {
            if (colliders[i].tag == "Killer")
            {
                // �����ȿ� �ִٸ� true����, ���ٸ� false
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
        Debug.Log("���θ��� ���� �޸���. TraceKiller");
        // ���θ��� ���� �޸���.
        surv_Stat.surPos = transform.position;
        surv_Stat.surPos = Vector3.MoveTowards(surv_Stat.surPos, target.transform.position, (surv_Stat.moveSpeed) * Time.deltaTime);
        transform.position = surv_Stat.surPos;
        transform.LookAt(target.transform.position);
    }

    private void AttackKiller()
    {
        // isAttack�� true�� ������ �ִϸ��̼�
        if(isAttackCoroutine == false)
        {
            StartCoroutine("CAttackKiller");
        }
    }

    IEnumerator CAttackKiller()
    {
        isAttackCoroutine = true;
        Debug.Log("���θ��� ������. AttackKiller");

        transform.LookAt(target.transform.position);
        surv_Stat.survAnimator.SetBool("BAttack", isAttack);

        yield return new WaitForSeconds(0.45f);
        surv_Weapon.enabled = true;
        yield return new WaitForSeconds(0.15f);
        surv_Weapon.enabled = false;

        yield return new WaitForSeconds(0.05f);
        isAttackCoroutine = false;
    }


    // ������ ���� ó�� �Լ�
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

        Debug.Log("hp���� ���");
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
