using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    public bool isEmpty; // �����ڰ� �ִ��� ������ Ȯ�� �ϴ� ����

    private SurvivorStat survivorStat;

    private void Start()
    {
        isEmpty = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        // �������� ���
        if(collision.gameObject.tag== "Survivor")
        {
            // ĳ����� ����ִٸ�
            if (isEmpty == true)
            {
                // space�� ���� ��� 
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // �ȿ� ������ ��� ���� �ִϸ��̼�
                    // �ȿ� ����� �ֱ� ������, false�� �ٲٰ� �ش� �������� ������ �����´�.
                    isEmpty= false;
                    survivorStat = collision.gameObject.GetComponent<SurvivorStat>();
                }
                 
                // shift�� space�� ���� ���� ���
                if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space) )
                {
                    // ������ ���� �ִϸ��̼�
                    isEmpty = false;
                    survivorStat = collision.gameObject.GetComponent<SurvivorStat>();
                }
            }
            // ĳ����� ������� �ʴٸ�
            else
            {
                // ������ �ڱ��ڽ��� �����ִ� ���
                if (survivorStat.gameObject == collision.gameObject)
                {
                    // space�� ���� ���
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        // ������ ������ �ִϸ��̼�
                        isEmpty = true;
                        survivorStat = null;
                    }

                    if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
                    {
                        // ������ ������ �ִϸ��̼�
                        isEmpty = true;
                        survivorStat = null;
                    }
                }

                // �����ٰ� �ݴ� �ִϸ��̼� �߰�
            }
            
        }


        // ���θ��� ���
        if (collision.gameObject.tag == "Killer")
        { 
            // ����ִ� ����
            if(isEmpty == true)
            {
                // space�� ���� ���
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    // �����ٰ� �ݴ� �ִϸ��̼�
                }
                
            }
            // �����ڰ� �ִٸ�
            else
            { 
               // space�� �������
               if(Input.GetKeyDown(KeyCode.Space))
               {
                    // �����ڸ� ���� �ִϸ��̼�
                   
                    KillerStat killerStat = collision.gameObject.GetComponent<KillerStat>();
                    // �����ڴ� ���θ����� ������ ������
                    // �������� �ǰ����´� 0�̵ȴ�.
                    survivorStat.health = 0;

                    // �����ڸ� �����ִ� ���·� �ٲ۴�.
                    killerStat.isCarry = true;
               }
            }
        }

    }
}
