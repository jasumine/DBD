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
                // �������� ��� ���� ����
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
        // �������� �������� �����Ѵ�.
        int itemNum = Random.Range(0, items.Count);
        GameObject itemObject = Instantiate(items[itemNum], this.transform);
        itemObject.transform.position = new Vector3(itemObject.transform.position.x, itemObject.transform.position.y + 0.5f, itemObject.transform.position.z);

        // �ڽ� �Ѳ��� ������鼭 �������� ���̰����ش�.
        boxTop.SetActive(false);
        boxBottom.SetActive(false);



        Debug.Log("�������� ȹ���߽��ϴ�!");
    }
}
