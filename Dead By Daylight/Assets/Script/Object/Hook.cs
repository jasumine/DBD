using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private HookGauge survivor;

    public bool isHanging = false;

    public bool GetIsHanging()
    {
        return isHanging;
    }


    public void SetIsHanging(SurvivorController _survivor)
    {
        isHanging = true;
        survivor = _survivor.hookGauge;

        // 갈고리에 걸렸다
        survivor.SetIsHang();
        survivor.myHook = this;
    }
    


}
