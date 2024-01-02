using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkillEffect
{
    void Use(GameObject obj);
}

public class SelfHealth:SkillEffect
{
    SurvivorStat stat;

    public void Use(GameObject obj)
    {
        stat = obj.GetComponent<SurvivorStat>();

    }
}








public class SkillEffectManager : MonoBehaviour
{
    
}
