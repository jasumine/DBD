using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public float openValue;

    public bool isComplete = false;

    public GameObject boxTop;
    public GameObject boxBottom;
    public List<GameObject> items;


    void Start()
    {
        openValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerStay(Collider other)
    {
        if (isComplete == false)
        {
            if (other.gameObject.tag == "Survivor")
            {
                // 생존자인 경우 상자 오픈
                if (Input.GetMouseButton(0))
                {

                    openValue += 30f * Time.deltaTime;
                    if (openValue >= 100)
                    {
                        isComplete = true;
                        GetItem();
                    }
                }
                else
                {
                    openValue = 0;
                }

            }
        }

    }
  

    private void GetItem()
    {
        // 랜덤으로 아이템을 생성한다.
        int itemNum = Random.Range(0, items.Count);
        GameObject itemObject = Instantiate(items[itemNum], this.transform);
        itemObject.transform.position = new Vector3(itemObject.transform.position.x, itemObject.transform.position.y + 0.5f, itemObject.transform.position.z);

        // 박스 뚜껑이 사라지면서 아이템이 보이게해준다.
        boxTop.SetActive(false);
        boxBottom.SetActive(false);



        Debug.Log("아이템을 획득했습니다!");
    }
}
