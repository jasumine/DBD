using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private HookGauge survivor;

    private bool isHanging = false;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public bool GetIsHanging()
    {
        return isHanging;
    }


    public void SetIsHanging(SurvivorController _survivor)
    {
        isHanging = true;
        survivor = _survivor.hookGauge;

        // ������ �ɷȴ�
        survivor.SetIsHang();
    }
    


}
