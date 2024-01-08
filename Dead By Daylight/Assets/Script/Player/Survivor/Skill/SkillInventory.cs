using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillInventory : MonoBehaviour
{
    public List<SkillData> skillList = new List<SkillData>();

    public List<SkillData> equipList = new List<SkillData>();

    private SkillDataManager skillDataManager;

    private void Start()
    {
        skillDataManager = GetComponent<SkillDataManager>();
    }

    private void Update()
    {
        AddInventory();
        AddEquip();
        RemoveEquip();
        UseSkill();
        UnUseSkill();
    }
    public void AddInventory()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            // 만약 같은 skill이 들어온다면, 레벨up하고, 아닌경우 skill을 추가
            
            for(int i = 0; i<skillList.Count; i++)
            {
                if (skillList[i] == skillDataManager.GetSKillData(0))
                {
                    skillList[i].skillLevel++;
                    return;
                }
            }

            Debug.Log("Inventory에 new skill추가");
            skillList.Add(skillDataManager.GetSKillData(0));
        }
    }


    public void AddEquip()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("Equip에 skill추가");
            equipList.Add(skillList[0]);
        }
    }
    
    public void RemoveEquip()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Debug.Log("Equip에 skill제거");
            equipList.Remove(skillList[0]);
            
        }
    }

    public void UseSkill()
    {
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            for (int i = 0; i < equipList.Count; i++)
            {
                Debug.Log("Equip Skill Use");
                equipList[i].effect.Use(this.gameObject, equipList[i]);
            }
        }
    }

    public void UnUseSkill()
    {
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            for (int i = 0; i < equipList.Count; i++)
            {
                Debug.Log("Equip Skill UnUse");
                equipList[i].effect.UnUse(this.gameObject, equipList[i]);
            }
        }
    }

}