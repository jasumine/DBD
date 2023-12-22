using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryBack : MonoBehaviour
{
    public bool isCarryCan = false;
    public bool isCarry = false;
    public Vector3 offset;

    private SurvivorController survivorController;
    private GameObject survivor;
    private Hook hook;

    Collider checkBox;

    void Start()
    {
        checkBox = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isCarryCan == true && Input.GetKeyDown(KeyCode.Space))
        {
            // 생존자가 움직일 수 없도록 한다.
            survivorController.isSurvMove = false;
            survivorController.isCarried = true;

            // 생존자를 업고 같이 이동해야함.
            isCarry = true;
        }

        if(isCarry==true)
        {
            isCarryCan = false;
            survivor.transform.position = transform.position + offset;
            Collider collider = survivor.gameObject.GetComponent<Collider>();
            Rigidbody rigid = survivor.gameObject.GetComponent<Rigidbody>();
            collider.enabled = false;   
            rigid.useGravity = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Survivor")
        {
            survivorController = collision.gameObject.GetComponent<SurvivorController>();

            survivor = survivorController.gameObject;

            if (survivorController.GetHealth() == 0 && isCarry == false)
            {
                isCarryCan = true;
            }
            else
            {
                isCarryCan = false;
            }
        }

        if(collision.gameObject.tag == "Hook")
        {
            hook = collision.gameObject.GetComponent<Hook>();  
            if(hook.GetIsHanging() == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    HangOnHook();
                }
            }
        }
    }

    private void HangOnHook()
    {
        Debug.Log("hangOnHook");
        isCarry = false;
        survivor.transform.position = hook.transform.position + new Vector3(0, 0.5f, -0.5f);

        hook.SetIsHanging(survivorController);


        Collider collider = survivor.gameObject.GetComponent<Collider>();
        Rigidbody rigid = survivor.gameObject.GetComponent<Rigidbody>();
        collider.enabled = true;
        rigid.useGravity = false;

        survivorController = null;
        survivor = null;
        isCarryCan = false;
    }



    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Survivor")
        {
            survivorController = null;
            survivor = null;
            isCarryCan = false;
        }
    }
}
