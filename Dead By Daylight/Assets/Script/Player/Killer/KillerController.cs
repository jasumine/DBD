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

    //  ������ �Լ�
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
        // ���θ��� �ൿ������ ������
        // ��Ŭ���� �ϸ� Ÿ���� �����Ѵ�.
        if(!killer_Stat.isActive && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack()) ;
        }

        // ���θ��� �ൿ������ ������
        // ��Ŭ���� ��� ������ �ִٸ�� Ư�� �ɷ��� �����Ѵ�.
        if(!killer_Stat.isActive && Input.GetMouseButton(1))
        {
            // Ư���ɷ��� 2�ʵ��� ������ �ִٸ� ����ǵ����Ѵ�.
            killer_Stat.curSpecialAbility += Time.deltaTime * killer_Stat.specialAbilitySpeed;
            if(killer_Stat.curSpecialAbility >= killer_Stat.maxSpecialAbility)
            {
                StartCoroutine("SpecialAbility");
            }
        }
        // �߰��� ���콺�� ���ٸ� �ٽ� 0���� �����Ѵ�.
        else
        {
            killer_Stat.curSpecialAbility = 0;
        }
    }

    IEnumerator Attack()
    {
        killer_Stat.isActive = true;
        // animator ��Ŭ�� trigger;
        Debug.Log("��Ŭ�� Ÿ�� ����");

        // ��Ŭ���� ������ �� Weapon collider�� active �ǵ��� �ϱ�
        // active�� �Ǹ� ����� ���� �������� hp�� ���ҵǵ��� ��.
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(killer_Stat.activeCount);
        weaponCollider.enabled = false;

        // �����ൿ�� ���� ���ϵ��� ��Ÿ���� �����Ŀ� false�� �ǵ��� ������ �ɾ��.
        killer_Stat.isActive = false;
    }

    IEnumerator SpecialAbility()
    {
        killer_Stat.isActive = true;
        // �ش� �ɷ��� ����Ѵ�.
        // Trigger�� �ִϸ��̼� ó��
        Debug.Log("Ư���ɷ� ����");
        yield return new WaitForSeconds(killer_Stat.activeCount);
        killer_Stat.isActive = false;
    }

}
