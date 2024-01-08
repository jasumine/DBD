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
            // ���� ���� skill�� ���´ٸ�, ����up�ϰ�, �ƴѰ�� skill�� �߰�
            
            for(int i = 0; i<skillList.Count; i++)
            {
                if (skillList[i] == skillDataManager.GetSKillData(0))
                {
                    skillList[i].skillLevel++;
                    return;
                }
            }

            Debug.Log("Inventory�� new skill�߰�");
            skillList.Add(skillDataManager.GetSKillData(0));
        }
    }


    public void AddEquip()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("Equip�� skill�߰�");
            equipList.Add(skillList[0]);
        }
    }
    
    public void RemoveEquip()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Debug.Log("Equip�� skill����");
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