using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillEffect
{
    void Use(GameObject obj, SkillData data);
    void UnUse(GameObject obj, SkillData data);
}

// =======���� �Ʒ��� ��ų�� �߰��Ѵ�========
[System.Serializable]
public class SelfCare: ISkillEffect
{
    SurvivorStat stat;

    public void Use(GameObject obj, SkillData data)
    {
        stat = obj.GetComponent<SurvivorStat>();
        stat.isHurtCareSelf = true;

        switch(data.skillLevel)
        {
            case 1:
                stat.speedHurtCare = data.LevelOneValue;
                break;

            case 2:
                stat.speedHurtCare = data.LevelTwoValue;
                break;

            case 3:
                stat.speedHurtCare = data.LevelThreeValue;
                break;

        }
    }
    public void UnUse(GameObject obj, SkillData data)
    {
        stat = obj.GetComponent<SurvivorStat>();
        stat.isHurtCareSelf = false;

        stat.speedHurtCare = 20;
    }
}

[System.Serializable]
public class UnBreakable : ISkillEffect
{
    SurvivorStat stat;
    public void Use(GameObject obj, SkillData data)
    {
        stat = obj.GetComponent<SurvivorStat>();
        stat.isVeryHurtCareSelf = true;
        switch (data.skillLevel)
        {
            case 1:
                stat.speedHurtCare = data.LevelOneValue;
                break;

            case 2:
                stat.speedHurtCare = data.LevelTwoValue;
                break;

            case 3:
                stat.speedHurtCare = data.LevelThreeValue;
                break;

        }
    }
    public void UnUse(GameObject obj, SkillData data)
    {
        stat = obj.GetComponent<SurvivorStat>();
        stat.isVeryHurtCareSelf = false;
        stat.speedVeryHurtCare = 10;
    }
}


public class SkillEffectManager : MonoBehaviour
{
    public List<ISkillEffect> skillEffects;

    public ISkillEffect GetEffect(int _idx)
    {
       return skillEffects[_idx];
    }

    private void Start()
    {
        InitSkillEffect();
    }

    void InitSkillEffect()
    {
        skillEffects = new List<ISkillEffect>()
        {
            new SelfCare(),
            new UnBreakable()
        };
    }

    void AddSkillEffect()
    {
        skillEffects.Add(new SelfCare());
    }
}
