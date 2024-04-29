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
        // �����ڸ� ������ �ִ� �����϶�, space�� ������ �����ڸ� �� �� �ִ�.
        if (killer_Stat.isCarryCan == true && Input.GetKeyDown(KeyCode.Space))
        {
            // �����ڰ� ������ �� ������ �Ѵ�.
            survivorController.isSurvMove = false;
            survivorController.isCarried = true;

            // �����ڸ� ���� ���� �̵��ؾ���.
            killer_Stat.isCarry = true;
        }

        // �����ڰ� �����ִ� ���¶��
        if(killer_Stat.isCarry ==true)
        {
            // �θ��� �����ڸ� �� �� ������ fal���ְ�
            // �������� ��ġ�� ų���� ��ġ�� ���� �̵��ǵ��� ���ش�.
            killer_Stat.isCarryCan = false;
            survivor.transform.position = transform.position + killer_Stat.offset;

            // �����̴� ���� �浹�� �Ͼ�� �ʰ�, �߷��� ���� �޾� �Ʒ��� �������� �ʰ� ���ش�.
            // ��� �浹���� �ʰ� �ϱ� ���� collider�� ���ش�.
            Collider collider = survivor.gameObject.GetComponent<Collider>();
            Rigidbody rigid = survivor.gameObject.GetComponent<Rigidbody>();
            collider.enabled = false;   
            rigid.useGravity = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // �����ڿ� �ε��� �ִ� ����
        if (collision.gameObject.tag == "Survivor")
        {
            // �ش� �������� ������ �����´�.
            survivorController = collision.gameObject.GetComponent<SurvivorController>();

            SurvivorStat survStat = survivorController.gameObject.GetComponent<SurvivorStat>();

            survivor = survivorController.gameObject;

            // �ش� �������� �ǰ����°� 0�̰�, ���θ��� �������� ��� ���� ������
            // ���θ��� �ش� �����ڸ� ���� �� �ֵ��� Ȱ��ȭ �ȴ�.
            if (survStat.health == 0 && killer_Stat.isCarry == false)
            {
                killer_Stat.isCarryCan = true;
            }
            else
            {
                killer_Stat.isCarryCan = false;
            }
        }

        // ������ �ε����ٸ�
        if(collision.gameObject.tag == "Hook")
        {
            // �ش� ������ ������ �޾ƿ´�.
            hook = collision.gameObject.GetComponent<Hook>();  

            // ������ ������ �ɷ����� �ʴٸ�(����ִ�)
            // space�� ������ ������ �����ڸ� �Ŵ� �Լ��� �����Ѵ�.
            if(hook.GetIsHanging() == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    HangOnHook();
                }
            }
        }
    }


    // ������ ��ȣ�ۿ� �ϴ� �Լ�
    private void HangOnHook()
    {
        Debug.Log("hangOnHook");
        // ���θ��� �����ڸ� ��� ���� ���� ���°� �ǰ�,
        // ���θ��� ��ġ�� ������ġ�� �������� �ɸ����°� �ȴ�.
        killer_Stat.isCarry = false;
        survivor.transform.position = hook.transform.position + new Vector3(0, 0.5f, -0.5f);

        // ��ũ�� �������� ������ �־��ش�.
        hook.SetIsHanging(survivorController);

        // �������� collider�� Ȱ��ȭ ��Ű�� rigidbody�� ��Ȱ��ȭ ��Ų��(�߷��ۿ�X) 
        Collider collider = survivor.gameObject.GetComponent<Collider>();
        Rigidbody rigid = survivor.gameObject.GetComponent<Rigidbody>();
        collider.enabled = true;
        rigid.useGravity = false;

        // �����ڸ� �ɾ��� ������, ���θ��� ���̻� �������� ������ ������ �ʵ��� ����ְ�
        // ���θ��� �����ڸ� �� �� ������ �ٲپ��ش�.
        survivorController = null;
        survivor = null;
        killer_Stat.isCarryCan = false;
    }



    private void OnCollisionExit(Collision collision)
    {
        // �����ڿ� �浹�ߴٰ� ��� ��� 
        // �ش� �������� ������ ��� ������ ������ �ȵǱ� ������ null�� ���ش�.
        if (collision.gameObject.tag == "Survivor")
        {
            survivorController = null;
            survivor = null;
            killer_Stat.isCarryCan = false;
        }
    }
}
