using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    // ���ڸ� �������� �����ڰ� ������ ���� �ϱ� ���� ����
    public bool isUse;
    public float curUseCount;
    public float maxUseCount;


    private void Start()
    {
        isUse = false;
        curUseCount = 0;
        maxUseCount = 3;
    }

    private void Update()
    {
        // ������ �̿��ߴٸ�
        if (isUse == true)
        {
            curUseCount += Time.deltaTime;
            // maxCout���Ŀ� �̿밡���ϰ� �Ѵ�.
            if (curUseCount > maxUseCount)
            {
                isUse = false;
                curUseCount = 0;
            }
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        // �������� ���
        if (collision.gameObject.tag == "Survivor")
        {
            // ���� ��������� ���� ���
            if(isUse == false)
            {
                // space�� ������
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // ������ �Ѿ�� �ִϸ��̼�
                    isUse = true;
                }

                // shift + space�� ������
                if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
                {
                    // ������ �Ѿ�� �ִϸ��̼�
                    isUse = true;
                }
            }
        }


        // ���θ��� ���
        if (collision.gameObject.tag == "Killer")
        {
            // space�� ������
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // �Ѿ�� �ִϸ��̼�
                isUse = true;
            }
        }

    }
}
