using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;





public class KillerController : MonoBehaviour
{
    public float moveSpeed;
    public float specialAbilityCount = 0;
    private bool isKillMove = true;
    public bool isActive = false;

    public bool isAI = true;


    Rigidbody killRigid;
    Vector3 killPos;


    private void Start()
    {
        killRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isAI == false)
        {
            KillerMove();
            KillerAbillity();
        }
    }

    private void KillerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        killPos = new Vector3(x, 0f, z).normalized * moveSpeed * Time.deltaTime;
        killRigid.MovePosition(transform.position + killPos);
    }


    private void KillerAbillity()
    {
        if(!isActive && Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Attack");
        }

        if(!isActive && Input.GetMouseButton(1))
        {
            specialAbilityCount += Time.deltaTime;
            if(specialAbilityCount >= 2)
            {
                StartCoroutine("SpecialAbility");
            }
        }
        else
        {
            specialAbilityCount = 0;
        }
    }

    IEnumerator Attack()
    {
        isActive = true;
        // animator ��Ŭ�� trigger;
        Debug.Log("�ɷ� ����");
        
        yield return new WaitForSeconds(1f);

        isActive = false;
    }

    IEnumerator SpecialAbility()
    {
        isActive = true;
        // �ش� �ɷ��� ����Ѵ�.
        // Trigger�� �ִϸ��̼� ó��
        Debug.Log("Ư���ɷ� ����");
        yield return new WaitForSeconds(1f);
        isActive = false;
    }
}
