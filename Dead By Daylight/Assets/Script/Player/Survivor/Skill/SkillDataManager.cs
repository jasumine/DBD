using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillData : MonoBehaviour
{
    public string name;
    public string description;
    public Image image;
    public int skillLevel;
    public ISkillEffect effect;
    public int LevelOneValue;
    public int LevelTwoValue;
    public int LevelThreeValue;

    SkillData(string _name, string _description, int _LevelOneValue, int _LevelTwoValue, int _LevelThreeValue)
    {
        name = _name;
        description = _description;
        LevelOneValue = _LevelOneValue;
        LevelTwoValue = _LevelTwoValue;
        LevelThreeValue = _LevelThreeValue;

    }





}




public class SkillDataManager : MonoBehaviour
{
    SkillData data;
    
    void Data()
    {
        
    }
}
