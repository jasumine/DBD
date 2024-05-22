using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerStat : MonoBehaviour
{
    public bool isWarrios = false;

    public bool isAI = false;

    private bool isKillMove = true;
    public bool isActive = false; // 좌클릭과 우클릭이 동시에 실행되면 안되기때문에 bool조건을 추가한다.


    // ==========무쌍모드========

    public int health = 100;


    // =========================




    public float moveSpeed;
    public float activeCount = 0;
    public float specialActiveCount = 0;
    public float curSpecialAbility = 0; // 특수능력버튼을 유지하는 동안 올라가는 카운트
    public float maxSpecialAbility = 2;
    public float specialAbilitySpeed = 1;


    // ================ 갈고리 ==============
    public bool isCarryCan = false;
    public bool isCarry = false;
    public Vector3 offset;



}
