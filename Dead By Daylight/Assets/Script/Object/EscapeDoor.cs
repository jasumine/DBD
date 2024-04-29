using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EscapeDoor : MonoBehaviour
{
    // Ż�ⱸ ��ũ��Ʈ
    public bool isCanOpen = false;
    public bool isOpen = false;

    public float curDoorValue = 0;
    public float maxDoorValue = 100;


    public int generatorCount = 0;
    public List<Generator> generator_list;


    private void Start()
    {
        FindGenerator();
    }


    private void Update()
    {
        if (generatorCount == 5&& isOpen==false)
        {
            isCanOpen = true;
        }
        else
        {
            CheckGeneratorState();
        }
    }


    void FindGenerator()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 200f);

        for(int i = 0; i< colliders.Length; i++)
        {
            if (colliders[i].tag=="Generator")
            {
                Generator generator = colliders[i].GetComponent<Generator>();
                generator_list.Add(generator);
            }
        }

    }



    // �����Ⱑ ����ư����� Ȯ���ϴ� �Լ�.
    void CheckGeneratorState()
    {
        for(int i = 0; i< generator_list.Count; i++)
        {
            if (generator_list[i].isComplete==true)
            {
                // ������ ������ �Ϸ��ߴٸ� count�� �÷��ְ�, for���� ������ �ʵ��� �������ش�.
                generatorCount++;
                generator_list.RemoveAt(i);
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        // ���� �� �� �ְ� �ȴٸ�, ��ȣ�ۿ��� �����ϴ�.
        if (isCanOpen == true)
        {
            // �����ڰ� ��ȣ�ۿ��� ���
            // ���� �ִ�ġ���� �����ԵǸ� ���̻� ��ȣ�ۿ� ���� ���ϰ� can�� false�� �ϰ� open�� true�� ���ش�.
            if (other.gameObject.tag == "Survivor")
            {
                if (Input.GetMouseButton(0))
                {
                    curDoorValue += Time.deltaTime;
                    if (curDoorValue >= maxDoorValue)
                    {
                        isCanOpen = false;
                        isOpen = true;
                    }
                }
            }

            if (other.gameObject.tag == "Killer")
            {
                // ���θ��� ��ȣ�ۿ��� ��� 
                // �����ں��� �� ������ �������� �Ѵ�.
                if (Input.GetMouseButton(0))
                {
                    curDoorValue += Time.deltaTime;
                    if (curDoorValue >= maxDoorValue)
                    {
                        isCanOpen = false;
                        isOpen = true;
                    }
                }
            }
        }
    }
    

}
