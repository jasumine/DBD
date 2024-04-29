using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;





public class KillerController : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider;

    KillerStat killer_Stat;

    Rigidbody killRigid;
    Vector3 killPos;

    private void Start()
    {
        killer_Stat = GetComponent<KillerStat>();
        killRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (killer_Stat.isAI == false)
        {
            KillerMove();
            KillerAbillity();
        }

    }

    //  움직임 함수
    private void KillerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = transform.right * x;
        Vector3 moveVertical = transform.forward * z;

        killPos = (moveHorizontal + moveVertical).normalized * killer_Stat.moveSpeed * Time.deltaTime;
        killRigid.MovePosition(transform.position + killPos);
    }


    private void KillerAbillity()
    {
        // 살인마가 행동중이지 않을때
        // 좌클릭을 하면 타격을 실행한다.
        if(!killer_Stat.isActive && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack()) ;
        }

        // 살인마가 행동중이지 않을떄
        // 우클릭을 계속 누르고 있다면면 특수 능력을 실행한다.
        if(!killer_Stat.isActive && Input.GetMouseButton(1))
        {
            // 특수능력을 2초동안 누르고 있다면 실행되도록한다.
            killer_Stat.curSpecialAbility += Time.deltaTime * killer_Stat.specialAbilitySpeed;
            if(killer_Stat.curSpecialAbility >= killer_Stat.maxSpecialAbility)
            {
                StartCoroutine("SpecialAbility");
            }
        }
        // 중간에 마우스를 뗀다면 다시 0부터 시작한다.
        else
        {
            killer_Stat.curSpecialAbility = 0;
        }
    }

    IEnumerator Attack()
    {
        killer_Stat.isActive = true;
        // animator 좌클릭 trigger;
        Debug.Log("좌클릭 타격 실행");

        // 좌클릭을 눌렀을 때 Weapon collider가 active 되도록 하기
        // active가 되면 무기와 닿은 생존자의 hp가 감소되도록 함.
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(killer_Stat.activeCount);
        weaponCollider.enabled = false;

        // 연속행동을 하지 못하도록 쿨타임이 지난후에 false가 되도록 조건을 걸어둠.
        killer_Stat.isActive = false;
    }

    IEnumerator SpecialAbility()
    {
        killer_Stat.isActive = true;
        // 해당 능력을 사용한다.
        // Trigger로 애니메이션 처리
        Debug.Log("특수능력 실행");
        yield return new WaitForSeconds(killer_Stat.activeCount);
        killer_Stat.isActive = false;
    }

}
