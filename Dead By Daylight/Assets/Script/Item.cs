using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    ToolBox,
    MediKit,
    Flash
}

public class Item : MonoBehaviour
{
    public ItemType type;
    public float itemGrade; //  ���
    public float itemDurability; // ������
    public string itemName;
    public string itemDescription;

    private bool isUse;
    
    void Start()
    {
        SetItem(type);
    }

    void Update()
    {
        
    }

    void SetItem(ItemType _type)
    {
        switch(_type)
        {
            case ItemType.ToolBox:
                Debug.Log("toolBox");
                isUse = true;
                break;

            case ItemType.MediKit:
                Debug.Log("Medikit");
                isUse = true;
                break;

            case ItemType.Flash:
                Debug.Log("Flash");
                isUse = true;
                break;

           default:
                break;
        }
    }


    void CheckDurability()
    {
        if(itemDurability <= 0) 
        {
            isUse = false;
        }
    }

}
