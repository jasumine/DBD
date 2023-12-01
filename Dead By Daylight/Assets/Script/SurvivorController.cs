using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public enum PlayerStates
{
    Idle,
    Run,
    Sit
}

public class SurvivorController : MonoBehaviour
{
    public float moveSpeed;
    private float health = 2;
    public float delayTime;

    public bool isSurvMove = true;
    private bool isSurvWalk;
    private bool isSurvRun;
    private bool isSurvSit;
    private bool isSurvSitWalk;

    public bool ishavingItem;
    public bool isFrontItem;

    public GameObject havingItem;
    public Transform itemPutOnPos;

    Rigidbody surRigid;
    Vector3 surPos;

    PlayerStates playerState;

    private void Start()
    {
        surRigid = GetComponent<Rigidbody>();
        playerState = PlayerStates.Idle;
        isFrontItem = false;
    }

    private void Update()
    {
        SurvivorMove();
        CheckState();
        CheckHealth();
        CheckObject();


        if (ishavingItem == true && isSurvMove == true && isFrontItem==false)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(ThrowItem());

                Debug.Log("아이템 Throw");
            }
               
        }
    }


    private void SurvivorMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        switch (playerState)
        {
            case PlayerStates.Idle:
                surPos = new Vector3(x, 0f, z).normalized * moveSpeed * Time.deltaTime;
                surRigid.MovePosition(transform.position + surPos);
                break;
            case PlayerStates.Run:
                surPos = new Vector3(x, 0f, z).normalized * (moveSpeed *2.5f) * Time.deltaTime;
                surRigid.MovePosition(transform.position + surPos);
                break;
            case PlayerStates.Sit:
                Renderer render = GetComponent<Renderer>();
                render.material.color = Color.yellow;

                surPos = new Vector3(x, 0f, z).normalized * (moveSpeed*0.5f )* Time.deltaTime;
                surRigid.MovePosition(transform.position + surPos);
                break;


        }


    }

    private void CheckState()
    {
        float run = Input.GetAxis("Run");
        bool shiftInput = !Mathf.Approximately(run, 0f);

        float sit = Input.GetAxis("Sit");
        bool ctrlInput = !Mathf.Approximately(sit, 0f);

        if (shiftInput && ctrlInput)
        {
            playerState = PlayerStates.Sit;
        }
        else if (shiftInput)
        {
            playerState = PlayerStates.Run;
        }
        else if (ctrlInput)
        {
            playerState = PlayerStates.Sit;
        }
        else
        {
            playerState = PlayerStates.Idle;
            Renderer render = GetComponent<Renderer>();
            render.material.color = Color.white;
        }
    }


    private void CheckHealth()
    {
        switch(health)
        {
            case 2:
                break;
            case 1:
                break;
            case 0:
                break;
        }
    }

    public void Sethealth()
    {
        health--;
        Debug.Log("살인마의 공격으로 다쳤습니다.");
        Debug.Log(health);
        // 무적상태로 만들기
    }

    private void CheckObject()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + new Vector3(0, 0, 0.5f), 0.8f);

        for(int i=0; i<colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Item")
            {
                isFrontItem = true;
                break;
            }
            else
            {
                isFrontItem = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + new Vector3(0, 0, 0.5f), 0.8f);

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            Item _item = other.gameObject.GetComponent<Item>();

            Debug.Log(_item.itemName);

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (ishavingItem == true && isSurvMove == true && isFrontItem == true)
                {
                    StartCoroutine(SwapItem(other));

                }

                if (ishavingItem == false && isSurvMove == true)
                {
                    // 아이템이 없다면 획득한다.
                    StartCoroutine(AcquireItem(other));
                }
            }
        }
    }


    IEnumerator ThrowItem()
    {
        isSurvMove = false;
        ishavingItem = false;

        Vector3 ThrowItemPos = new Vector3(transform.position.x, 0.5f, transform.position.z + 2f);

        GameObject pasteItem = Instantiate(havingItem.gameObject, ThrowItemPos, Quaternion.identity);

        Collider collider = pasteItem.gameObject.GetComponent<Collider>();
        collider.enabled = true;

        Destroy(havingItem);

        delayTime = 1f;
        yield return new WaitForSeconds(delayTime);
        isSurvMove = true;
    }


    IEnumerator AcquireItem(Collider other)
    {
        isSurvMove = false;
        ishavingItem = true;

        GameObject copyItem = Instantiate(other.gameObject, itemPutOnPos);
        copyItem.transform.position = itemPutOnPos.position;

        havingItem = copyItem;

        Collider collider = havingItem.gameObject.GetComponent<Collider>();
        collider.enabled = false;

        Destroy(other.gameObject);

        Debug.Log("아이템을 획득했다.");

        delayTime = 1f;
        yield return new WaitForSeconds(delayTime);
        isSurvMove = true;
    }

    IEnumerator SwapItem(Collider other)
    {
        isSurvMove = false;

        // 내가 가진 아이템을, 바꿀 아이템이 있는 위치에 버린다.
        Vector3 swapItemPos = other.transform.position;
        GameObject pasteItem = Instantiate(havingItem.gameObject, swapItemPos, Quaternion.identity);
        Collider collider = pasteItem.gameObject.GetComponent<Collider>();
        collider.enabled = true;
        // 원래 가지고있는 아이템을 파괴한다.
        Destroy(havingItem.gameObject);


        // 바꿀 아이템을 획득한다.
        pasteItem = Instantiate(other.gameObject, itemPutOnPos);
        pasteItem.transform.position = itemPutOnPos.position;
        havingItem = pasteItem;

        collider = havingItem.gameObject.GetComponent<Collider>();
        collider.enabled = false;

        // 바닥에있는 아이템을 파괴한다.
        Destroy (other.gameObject);
        Debug.Log("아이템을 교체했다.");


        delayTime = 1f;
        yield return new WaitForSeconds(delayTime);
        isSurvMove = true;
    }

}
