using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkillData 
{
    public string skillName;
    public string description;
    public Image image;
    public int skillLevel;
    public ISkillEffect effect;
    public bool isEffectOn;
    public int LevelOneValue;
    public int LevelTwoValue;
    public int LevelThreeValue;
    public int point;

    public SkillData(string _name, string _description, ISkillEffect _effect, int _LevelOneValue, int _LevelTwoValue, int _LevelThreeValue)
    {
        skillName = _name;
        description = _description;
        effect = _effect;
        LevelOneValue = _LevelOneValue;
        LevelTwoValue = _LevelTwoValue;
        LevelThreeValue = _LevelThreeValue;

        if(effect!= null ) { isEffectOn = true; }
    }

}


public class SkillDataManager : MonoBehaviour
{
    public List<SkillData> skillDatas;
    private SkillEffectManager skillEffectManager;


    private void Start()
    {
        skillEffectManager = GetComponent<SkillEffectManager>();
        InitSkillData();
    }

    public SkillData GetSKillData(int _index)
    {
        return skillDatas[_index];
    }


    private void InitSkillData()
    {
        skillDatas = new List<SkillData>();
        Debug.Log("스킬초기화");

        skillDatas.Add(new SkillData("SelfCare", "survivor can selfCaring", skillEffectManager.GetEffect(0), 30, 40, 50));

        skillDatas.Add(new SkillData("Sprint", "survivor can runaway for 3sec", skillEffectManager.GetEffect(0), 30, 40, 50));

        skillDatas.Add(new SkillData("UnBreakble","Survivor Can Get Up Self for One Time", skillEffectManager.GetEffect(1), 30, 40, 50));
    }

    
}
