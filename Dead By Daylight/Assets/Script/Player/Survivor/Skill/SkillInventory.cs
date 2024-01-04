using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInventory : MonoBehaviour
{
    List<SkillData> skillList = new List<SkillData>();


    public void AddSkill(SkillData _skill)
    {
        skillList.Add(_skill);
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class SkillEquip :MonoBehaviour
{
    List<SkillData> useSkillList = new List<SkillData>();

    public void AddSkill(SkillData _skill)
    {
        useSkillList.Add(_skill);
    }

    public void RemoveSKill(int idx)
    {
        useSkillList[idx] = null;
    }

    public void UseSKill()
    {
        for(int i =0; i < useSkillList.Count; i++)
        {
            useSkillList[i].effect.Use(this.gameObject, useSkillList[i]);
        }
    }

    public void UnUseSkill()
    {
        for (int i = 0; i < useSkillList.Count; i++)
        {
            useSkillList[i].effect.UnUse(this.gameObject, useSkillList[i]);
        }
    }
}
