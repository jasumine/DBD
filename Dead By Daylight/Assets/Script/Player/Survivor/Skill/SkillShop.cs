using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillShop : MonoBehaviour
{
    public int bloodPoint;
    private SkillDataManager skillDataManager;
    private SkillInventory inventory;


    private void Start()
    {
        skillDataManager = GetComponent<SkillDataManager>();
        inventory = GetComponent<SkillInventory>();
    }

    private void Update()
    {
        
    }

    private void ShowItem()
    {
        if(Input.GetKeyDown(KeyCode.Keypad7))
        {
            skillDataManager.GetSKillData(0);
        }

    }

    private void Buy()
    {

    }

}
