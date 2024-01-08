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
    public float itemGrade; //  등급
    public float itemDurability; // 내구도
    public string itemName;
    public string itemDescription;

    public bool isUse;
    
    void Start()
    {
        SetItem(type);
    }

    void Update()
    {
        CheckDurability();
    }

 

    void SetItem(ItemType _type)
    {
        switch(_type)
        {
            case ItemType.ToolBox:
                isUse = true;
                break;

            case ItemType.MediKit:
                isUse = true;
                break;

            case ItemType.Flash:
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
