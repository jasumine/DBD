using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerStates
{
    Idle,
    Run,
    Sit
}

public class SurvivorController : MonoBehaviour
{
    SurvivorStat surv_Stat;

    public bool isSurvMove = true;
    public bool isCarried = false;
    private bool isSurvWalk;
    private bool isSurvRun;
    private bool isSurvSit;
    private bool isSurvSitWalk;

    private bool ishavingItem;
    private bool isFrontItem;
    public bool isSuperMode = false;

    float struggleValueMax = 100;
    float struggle = 0;
    bool isA = false;
    bool isD = true;


    public GameObject havingItem;
    public Transform itemPutOnPos;

    public HookGauge hookGauge;

    Vector3 surPos;
    PlayerStates playerState;

    private void Start()
    {
        surv_Stat = GetComponent<SurvivorStat>();
        surv_Stat.surRigid = GetComponent<Rigidbody>();
        playerState = PlayerStates.Idle;
        isFrontItem = false;    
    }

    private void Update()
    {
        if (surv_Stat.isAI == false)
        {
            SurvivorMove();
            CheckState();
            CheckObject();
            CheckHealth();
            UsingItem();

            if (ishavingItem == true && isSurvMove == true && isFrontItem == false)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StartCoroutine(ThrowItem());

                    Debug.Log("������ Throw");
                }

            }
        }
        else
        {
            CheckState();
            CheckHealth();

        }

    }

    


    private void SurvivorMove()
    {
        if (isSurvMove == true)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 moveHorizontal = transform.right * x;
            Vector3 moveVertical = transform.forward * z;


            switch (playerState)
            {
                case PlayerStates.Idle:
                    surPos = (moveHorizontal +  moveVertical).normalized * surv_Stat.moveSpeed * Time.deltaTime;
                    surv_Stat.surRigid.MovePosition(transform.position + surPos);

                    //Debug.Log(this.gameObject + "survivor Idle");

                    break;
                case PlayerStates.Run:
                    Renderer render = GetComponent<Renderer>();
                    render.material.color = Color.green;

                    surPos = (moveHorizontal + moveVertical).normalized * (surv_Stat.moveSpeed * 2.5f) * Time.deltaTime;
                    surv_Stat.surRigid.MovePosition(transform.position + surPos);

                   // Debug.Log(this.gameObject + "survivor Run");

                    break;
                case PlayerStates.Sit:
                    render = GetComponent<Renderer>();
                    render.material.color = Color.yellow;

                    surPos = new Vector3(x, 0f, z).normalized * (surv_Stat.moveSpeed * 0.5f) * Time.deltaTime;
                    surv_Stat.surRigid.MovePosition(transform.position + surPos);
                    break;
            }
        }

        if(isCarried == true)
        {
            // a d�� ������ �߹��� ģ��.
            if(struggle < struggleValueMax)
            {
                if (Input.GetKeyDown(KeyCode.A) && isA == false && isD == true)
                {

                }
                if (Input.GetKeyDown(KeyCode.D))
                {

                }
            }
            else
            {
                isCarried = false;

                // ų�����Լ� ���������� �ϱ�.
            }
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
        switch(surv_Stat.health)
        {
            case 2:
                break;
            case 1:
                
                break;
            case 0:
                surv_Stat.moveSpeed = 2;
                TreatSelp();
                break;
        }
    }

    public bool Sethealth()
    {
        if(isSuperMode == false && surv_Stat.health == 2)
        {
           surv_Stat.health--;
            Debug.Log("���θ��� �������� ���ƽ��ϴ�.");
            Debug.Log(surv_Stat.health);
            StartCoroutine(SuperMode());

            return true;
        }
        if (isSuperMode == false && surv_Stat.health == 1)
        {
            surv_Stat.health--;
            Debug.Log("���θ��� �������� ���ƽ��ϴ�.");
            Debug.Log(surv_Stat.health);

            return true;
        }
        else
        {
            return false;
        }
    }

    private void TreatSelp()
    {
        if(Input.GetMouseButton(0))
        {
            if(surv_Stat.health == 0)
            {
                surv_Stat.currentHealth += surv_Stat.speedVeryHurtCare * Time.deltaTime;
                if(surv_Stat.isVeryHurtCareSelf == false)
                {
                    if (surv_Stat.currentHealth >= 95)
                    {
                        surv_Stat.currentHealth = 95;
                    }
                }
               else
                {
                    if (surv_Stat.currentHealth >= surv_Stat.maxHealth)
                    {
                        surv_Stat.health++;
                    }
                }
            }
            
            if(surv_Stat.health==1 && surv_Stat.isHurtCareSelf == true)
            {
                surv_Stat.currentHealth += surv_Stat.speedHurtCare * Time.deltaTime;
                if (surv_Stat.currentHealth >= surv_Stat.maxHealth)
                {
                    surv_Stat.health++;
                }
            }
        }
    }

    IEnumerator SuperMode()
    {
        isSuperMode = true;
        // �ٸ� ������Ʈ������ �ε�����, ������, ���θ�, ���θ������ ����ϵ��� ���̾� ����
        this.gameObject.layer = 8;
        surv_Stat.moveSpeed += 5;
        Debug.Log(surv_Stat.moveSpeed);

        yield return new WaitForSeconds(2f);
        surv_Stat.moveSpeed -= 5;
        isSuperMode = false;
        Debug.Log(surv_Stat.moveSpeed);
        this.gameObject.layer = 6; // survivor layer
    }

    // ===================Item==============================

    private void UsingItem()
    {
        if(ishavingItem == true)
        {
            Item item = havingItem.GetComponent<Item>();
            if (item.isUse == true &&Input.GetMouseButton(1))
            {
                item.itemDurability -= 1 * Time.deltaTime;
            }
        }
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

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z+0.5f), this.transform.localScale);
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
                    // �������� ���ٸ� ȹ���Ѵ�.
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

        surv_Stat.delayTime = 1f;
        yield return new WaitForSeconds(surv_Stat.delayTime);
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

        Debug.Log("�������� ȹ���ߴ�.");

        surv_Stat.delayTime = 1f;
        yield return new WaitForSeconds(surv_Stat.delayTime);
        isSurvMove = true;
    }

    IEnumerator SwapItem(Collider other)
    {
        isSurvMove = false;

        // ���� ���� ��������, �ٲ� �������� �ִ� ��ġ�� ������.
        Vector3 swapItemPos = other.transform.position;
        GameObject pasteItem = Instantiate(havingItem.gameObject, swapItemPos, Quaternion.identity);
        Collider collider = pasteItem.gameObject.GetComponent<Collider>();
        collider.enabled = true;
        // ���� �������ִ� �������� �ı��Ѵ�.
        Destroy(havingItem.gameObject);


        // �ٲ� �������� ȹ���Ѵ�.
        pasteItem = Instantiate(other.gameObject, itemPutOnPos);
        pasteItem.transform.position = itemPutOnPos.position;
        havingItem = pasteItem;

        collider = havingItem.gameObject.GetComponent<Collider>();
        collider.enabled = false;

        // �ٴڿ��ִ� �������� �ı��Ѵ�.
        Destroy (other.gameObject);
        Debug.Log("�������� ��ü�ߴ�.");


        surv_Stat.delayTime = 1f;
        yield return new WaitForSeconds(surv_Stat.delayTime);
        isSurvMove = true;
    }

}
