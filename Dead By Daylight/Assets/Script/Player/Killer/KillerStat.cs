using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerStat : MonoBehaviour
{
    public bool isWarrios = false;

    public bool isAI = false;

    private bool isKillMove = true;
    public bool isActive = false; // ��Ŭ���� ��Ŭ���� ���ÿ� ����Ǹ� �ȵǱ⶧���� bool������ �߰��Ѵ�.


    // ==========���ָ��========

    public int health = 100;


    // =========================




    public float moveSpeed;
    public float activeCount = 0;
    public float specialActiveCount = 0;
    public float curSpecialAbility = 0; // Ư���ɷ¹�ư�� �����ϴ� ���� �ö󰡴� ī��Ʈ
    public float maxSpecialAbility = 2;
    public float specialAbilitySpeed = 1;


    // ================ ���� ==============
    public bool isCarryCan = false;
    public bool isCarry = false;
    public Vector3 offset;



}
