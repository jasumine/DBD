using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EscapeDoor : MonoBehaviour
{
    // 탈출구 스크립트
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



    // 발전기가 몇개돌아갔는지 확인하는 함수.
    void CheckGeneratorState()
    {
        for(int i = 0; i< generator_list.Count; i++)
        {
            if (generator_list[i].isComplete==true)
            {
                // 발전기 수리를 완료했다면 count를 올려주고, for문에 들어오지 않도록 제거해준다.
                generatorCount++;
                generator_list.RemoveAt(i);
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        // 문을 열 수 있게 된다면, 상호작용이 가능하다.
        if (isCanOpen == true)
        {
            // 생존자가 상호작용할 경우
            // 문이 최대치까지 열리게되면 더이상 상호작용 하지 못하게 can을 false로 하고 open을 true로 해준다.
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
                // 살인마가 상호작용할 경우 
                // 생존자보다 더 빠르게 열리도록 한다.
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
