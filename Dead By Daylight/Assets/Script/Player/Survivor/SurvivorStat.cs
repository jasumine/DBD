using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorStat : MonoBehaviour
{
    public float moveSpeed = 10;

    public float delayTime = 0;

    // 치료관련 
    public int health = 2;
    public float currentHealth = 0;
    public float maxHealth = 100;
    public float speedHealth = 10;
    public bool isSuperMode = false;

    // 갈고리
    public float hook = 0;
    public float currentSave = 0;
    public float maxSave = 5;
    public float speedSave = 2;

    // 견자단
    public int hookChance = 0;
    public float currentHookValue = 100;

    public float currentEscape = 0;
    public float maxEscape = 3;
    public float speedEscape = 10;

    public bool isHang;
    public bool isTryEscape = false;



    // 몸부림
    public float strugle;
    public float currentStrugle = 0;
    public float maxStrugle = 100;
    public float speedStrugle = 0;
    public bool isInputA;
    public bool isInputD;
    

    // 



}
