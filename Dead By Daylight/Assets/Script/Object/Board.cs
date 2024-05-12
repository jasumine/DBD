using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Board : MonoBehaviour
{
    public bool isDown; // ���ڰ� ���������� Ȯ���� ���� ����

    // ���ڸ� �������� �����ڰ� ������ ���� �ϱ� ���� ����
    public bool isUse;
    public float curUseCount;
    public float maxUseCount;

    private void Start()
    {
        isDown = false;
        isUse = false;
        curUseCount = 0;
        maxUseCount = 1;
    }

    private void Update()
    {
        // ������ �̿��ߴٸ�
        if(isUse == true)
        {
            curUseCount += Time.deltaTime;
            // maxCout���Ŀ� �̿밡���ϰ� �Ѵ�.
            if(curUseCount > maxUseCount)
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
            // ���ڰ� ������ ���� ���� ���
            if(isDown == false)
            {
                // space�� ������ ���ڸ� ������.
                if(Input.GetKeyDown(KeyCode.Space)) 
                {
                    BoardDown();
                }
            }

            // ���ڰ� ������ ���
            else
            {
                // space�� ���� ���
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    // ���ڸ� õõ�� �̵��ϴ� �ִϸ��̼�

                    isUse = true;
                }

                // space�� shift�� �Բ� ���� ���
                if(Input.GetKeyDown (KeyCode.Space) && Input.GetKeyDown(KeyCode.LeftShift))
                {
                    // ���ڸ� ������ �̵��ϴ� �ִϸ��̼�

                    isUse = true;
                }
            }
        }


        // ���θ��� ���
        if (collision.gameObject.tag == "Killer")
        {
            // ���ڰ� �������ְ�, space�� ������
            if(isDown==true  && Input.GetKeyDown(KeyCode.Space))
            {
                // ���ڸ� �μ���.
                BoardBreak();
            }

        }

    }

    private void BoardDown()
    {
        isDown = true;
        // ���ڰ� �Ѿ����� �ִϸ��̼� ����

    }

    private void BoardBreak()
    {
        // ���� �μ����� �ִϸ��̼� �� ���� �����ϱ�

        gameObject.SetActive(false);
    }

}
