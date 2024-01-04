using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillEffect
{
    void Use(GameObject obj, SkillData data);
    void UnUse(GameObject obj, SkillData data);
}

// =======���� �Ʒ��� ��ų�� �߰��Ѵ�========
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
        stat.isVeryHurtCareSelf = false;

        stat.speedHurtCare = 20;
    }
}

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
    public enum effects{
        SelfCare,
        UnBreakable
    }

    public void GetEffect(int name)
    {
       
    }
}
